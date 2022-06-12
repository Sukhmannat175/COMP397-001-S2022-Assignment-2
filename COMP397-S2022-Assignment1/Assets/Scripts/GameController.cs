using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private GruntGolemController gruntGolemController;

    [Header("Enemies")]
    [SerializeField] private GameObject gruntGolem;
    [SerializeField] private GameObject WayPoints;
    [SerializeField] private Transform gruntGolemSpawn;

    [Header("UI")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject hpBar;
    [SerializeField] private Text finalScore;
    [SerializeField] private Text finalEnemiesKilled;

    public static GameController instance;
    [HideInInspector] public int score = 0;
    [HideInInspector] public int enemiesKilled = 0;
    [HideInInspector] public int totalEnemiesDead = 0;

    private bool spawn = true;
    
    private IEnumerator Spawn()
    {
        while (spawn)
        {
            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForSeconds(1);
                Instantiate(gruntGolem, gruntGolemSpawn.position, gruntGolem.transform.rotation);
            }
            spawn = false;
        }
    }

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
        Time.timeScale = 1;
        gruntGolemController = gruntGolem.GetComponent<GruntGolemController>();
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gruntGolemController.wayPoints.Length; i++)
        {
            gruntGolemController.wayPoints[i] = WayPoints.transform.GetChild(i);
            gruntGolemController.healthBarController = hpBar.GetComponent<HealthBarController>();
        }

        if (totalEnemiesDead == 20)
        {
            GameOver();
        }
    }

    // Methods
    public void GameOver()
    {
        finalScore.text = score.ToString();
        finalEnemiesKilled.text = enemiesKilled.ToString(); 
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
