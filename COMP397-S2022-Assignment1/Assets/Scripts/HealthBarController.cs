/*  Filename:       HealthBarController.cs
 *  Author:         Marcus Ngooi (301147411)
 *  Last Update:    June 11, 2022
 *  Description:    Controls player's health bar.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBarController : MonoBehaviour
{
    // "Public" variables
    [SerializeField][Range(0,10)] private int healthValue = 10;

    // Private variables
    private Slider bar;
    private int startingHealthValue;

    // Start is called before the first frame update
    void Start()
    {
        startingHealthValue = healthValue;
        bar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sets value of slider to healthValue
        if (healthValue != startingHealthValue)
        {
            startingHealthValue = healthValue;
            bar.value = healthValue;
        }

        if (healthValue == 0)
        {
            GameController.instance.GameOver();
        }

        // For testing
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1);
        }
    }

    // Methods
    public void TakeDamage(int damage)
    {
        healthValue -= damage;
    }
}
