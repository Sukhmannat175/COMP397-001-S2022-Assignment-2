/*  Filename:           GameController.cs
 *  Author:             Sukhmannat Singh (301168420)
 *  Last Update:        June 11, 2022
 *  Description:        Controls aspects of the game including spawning enemy waves, ending the game, keeping track of vital statistics.
 *  Revision History:   June 11, 2022 (Sukhmannat Singh): Initial script.
 *                      July 20, 2022 (Han Bi): Refactored code to work with EnemyFactory.cs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Enemy Waves")]
    [SerializeField] private Text waveLabel;
    [SerializeField] private string waveLabelFormat;
    [SerializeField] private float waveInterval = 10f;
    [SerializeField] private float spawnInterval = 1f;

    [Header("Enemies")]

    [SerializeField] private Transform wayPointsContainer;
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private Transform enemyContainer;

    [Header("UI")]
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private PlayerHealthBarController playerHpBarController;
    [SerializeField] private Text finalScore;
    [SerializeField] private Text finalEnemiesKilled;

    [Header("Loaded from Resources")]
    [SerializeField] List<EnemyWave> waveStaticData;

    [Header("Debug")]
    [SerializeField] private int currentWave;
    [SerializeField] private int score = 0;
    [SerializeField] private int enemiesKilled = 0;
    [Tooltip("Include those killed by towers and self-destructed when reached the end of the path")]
    [SerializeField] private int totalEnemiesDead = 0;
    [SerializeField] private int totalEnemiesInTheLevel;

    public static GameController instance;
    public SaveData current;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1;

        // Load data from scriptable object
        GameStaticData gameStaticData = Resources.Load<GameStaticData>("ScriptableObjects/GameStaticData");
        if (gameStaticData != null)
        {
            waveStaticData = gameStaticData.waveStaticData;
            waveInterval = gameStaticData.waveInterval;
            spawnInterval = gameStaticData.spawnInterval;
            //gruntGolemStaticData = gameStaticData.enemyStaticData.Find(x => x.enemy == Enemy.EnemyType.GRUNTGOLEM);
            //stoneMonsterStaticData = gameStaticData.enemyStaticData.Find(x => x.enemy == Enemy.EnemyType.STONEMONSTER);
            //resourcesStealerStaticData = gameStaticData.enemyStaticData.Find(x => x.enemy == Enemy.EnemyType.RESOURCESTEALER);
        }
        else {
            Debug.LogError("gameStaticData cannot be loaded");
        }

        CalculateTotalEnemiesInTheLevel();
        StartCoroutine(Spawn());
    }

    private void CalculateTotalEnemiesInTheLevel()
    {
        totalEnemiesInTheLevel = 0;
        foreach (EnemyWave wave in waveStaticData)
        {
            totalEnemiesInTheLevel += wave.types.Count;
        }
    }

    private void UpdateWaveLabel()
    {
        waveLabel.text = string.Format(waveLabelFormat, currentWave, waveStaticData.Count);
    }

    private IEnumerator Spawn()
    {
        foreach (EnemyWave wave in waveStaticData)
        {
            currentWave++;
            UpdateWaveLabel();

            foreach (Enemy.EnemyType type in wave.types)
            {
                yield return new WaitForSeconds(spawnInterval);

                Enemy enemy = null;

                enemy = EnemyFactory.instance.CreateEnemy(type, enemySpawnPoint.position);
                enemy.transform.parent = enemyContainer;

                if (enemy != null)
                    enemy.SetWayPoints(wayPointsContainer);
            }

            yield return new WaitForSeconds(waveInterval);
        }
    }

    public void KillEnemey(int score)
    {
        this.score += score;
        enemiesKilled += 1; 
        AddTotalEnemiesDead();
    }

    public void AddTotalEnemiesDead()
    {
        totalEnemiesDead += 1;
        CheckGameState();
    }

    public void CheckGameState()
    {
        if (totalEnemiesInTheLevel == totalEnemiesDead)
        {
            gameOverScreen.Open(GameOverScreen.GameEndState.VICTORY, score, enemiesKilled);
        }
    }

    public void GameOver()
    {
        finalScore.text = score.ToString();
        finalEnemiesKilled.text = enemiesKilled.ToString();
        gameOverScreen.Open(GameOverScreen.GameEndState.GAMEOVER, score, enemiesKilled);
    }

    public void OnSave()
    {
        SerializationController.Save("Save", this.current);
    }

    public void OnLoad()
    {
        /*SaveData current = SaveData.current;
        TowerPlacer placer =  new TowerPlacer();
        current = (SaveData)SerializationController.Load(Application.persistentDataPath + "/saves/Save.save");

        for (int i = 0; i < SaveData.current.towers.Count; i++)
        {
            TowerData currentTower = current.towers[i];
            placer.PlaceTower();
        }*/
    }
}
