using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static GameController instance;

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
        }
    }

    // Methods
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
