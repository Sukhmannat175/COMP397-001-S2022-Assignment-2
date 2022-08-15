/*  Filename:           PlayerHealthBarController.cs
 *  Author:             Marcus Ngooi (301147411)
 *  Last Update:        July 22, 2022
 *  Description:        Controls player's health bar.
 *  Revision History:   June 11, 2022 (Marcus Ngooi): Initial script.
 *                      June 18, 2022 (Yuk Yee Wong): Rename the script, add static instance, add get value methods, inherit the HealthDisplay
 *                      July 22, 2022 (Sukhmannat Singh): Updated the changing value of health bar slider
 */

using UnityEngine;

public class PlayerHealthBarController : HealthDisplay
{
    [SerializeField] private int playerMaxHealthValue = 10;
    // Private variables
    [SerializeField][Range(0, 10)] public int currentPlayerHealthValue;

    public static PlayerHealthBarController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealthValue = playerMaxHealthValue;
        currentPlayerHealthValue = maxHealthValue;
        Init(maxHealthValue);
    }

    // Update is called once per frame
    void Update()
    {
        // Sets value of slider to healthValue
        if (currentHealthValue != currentPlayerHealthValue)
        {
            SetHealthValue(currentPlayerHealthValue);
        }        

        // For testing
        if (Input.GetKeyDown(KeyCode.K) && currentPlayerHealthValue > 0)
        {
            currentPlayerHealthValue--;
        }
    }

    public override void ReduceHealth(int damage)
    {
        currentPlayerHealthValue--;
    }

    public override void ReachZeroHealth()
    {
        GameController.instance.GameOver();
    }
}
