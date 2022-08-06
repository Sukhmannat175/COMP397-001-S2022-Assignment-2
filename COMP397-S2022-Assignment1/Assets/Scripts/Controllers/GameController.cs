/*  Filename:           GameController.cs
 *  Author:             Sukhmannat Singh (301168420)
 *                      Yuk Yee Wong (301234795)
 *                      Han Bi (301176547)
 *                      Marcus Ngooi (301147411)
 *  Last Update:        August 5, 2022
 *  Description:        Controls aspects of the game including spawning enemy waves, ending the game, keeping track of vital statistics.
 *  Revision History:   June 11, 2022 (Sukhmannat Singh): Initial script.
 *                      June 26, 2022 (Yuk Yee Wong): Adding wave management scripts.
 *                      July 20, 2022 (Han Bi): Refactored code to work with EnemyFactory.cs.
 *                      July 22, 2022 (Sukhmannat Singh): Added Save/Load methods.
 *                      July 24, 2022 (Marcus Ngooi): Integrated Factory Design pattern with load system.
 *                      August 1, 2022 (Yuk Yee Wong): Modified Spawn method, adapted object pooling.
 *                      August 5, 2022 (Marcus Ngooi): Moved towersPlaced and maxTowersToBePlaced variables from TowerPlacer to GameController.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Enemy Waves")]
    [SerializeField] private Text waveLabel;
    [SerializeField] private string waveLabelFormat;
    [SerializeField] private float waveInterval = 10f;
    [SerializeField] private float spawnInterval = 1f;

    [Header("Enemies")]
    [SerializeField] private Enemy gruntGolemPrefab;
    [SerializeField] private Enemy stoneMonsterPrefab;
    [SerializeField] private Enemy resourcesStealerPrefab;
    [SerializeField] private Transform wayPointsContainer;
    [SerializeField] private Transform enemySpawnPoint;

    [Header("Towers")]
    [SerializeField] private int maxTowersToBePlaced = 10;
    [SerializeField] private int towersPlaced = 0;

    [Header("UI")]
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private PlayerHealthBarController playerHpBarController;
    [SerializeField] private Text finalScore;
    [SerializeField] private Text finalEnemiesKilled;
    [SerializeField] private GameObject saveDataButton1;
    [SerializeField] private GameObject saveDataButton2;
    [SerializeField] private GameObject loadDataButton1;
    [SerializeField] private GameObject loadDataButton2;

    [Header("Loaded from Resources")]
    [SerializeField] List<EnemyWave> waveStaticData;
    [SerializeField] private EnemyStaticData gruntGolemStaticData;
    [SerializeField] private EnemyStaticData stoneMonsterStaticData;
    [SerializeField] private EnemyStaticData resourcesStealerStaticData;
    [SerializeField] private TowerPlacer placer;

    [Header("Debug")]
    [SerializeField] private int currentWave;
    [SerializeField] private int score = 0;
    [SerializeField] private int enemiesKilled = 0;
    [SerializeField] private int enemiesSpawned = 0;
    [Tooltip("Include those killed by towers and self-destructed when reached the end of the path")]
    [SerializeField] private int totalEnemiesDead = 0;
    [SerializeField] private int totalEnemiesInTheLevel;

    // Properties
    public int TowersPlaced { get { return towersPlaced; } set { towersPlaced = value; } }
    public int MaxTowersToBePlaced { get { return maxTowersToBePlaced; } }

    public static GameController instance;
    private TowerPlacer towerPlacer;
    public SaveData current;
    private bool changeWaveOnLoad = true;

    private Coroutine spawnCoroutine = null;


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
            gruntGolemStaticData = gameStaticData.enemyStaticData.Find(x => x.enemy == Enemy.EnemyType.GRUNTGOLEM);
            stoneMonsterStaticData = gameStaticData.enemyStaticData.Find(x => x.enemy == Enemy.EnemyType.STONEMONSTER);
            resourcesStealerStaticData = gameStaticData.enemyStaticData.Find(x => x.enemy == Enemy.EnemyType.RESOURCESTEALER);
        }
        else {
            Debug.LogError("gameStaticData cannot be loaded");
        }

        CalculateTotalEnemiesInTheLevel();
        spawnCoroutine = StartCoroutine(Spawn(0, 0));

        towerPlacer = FindObjectOfType<TowerPlacer>();
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

    private IEnumerator Spawn(int enemiesSpawnedBefore, int currentWaveBefore)
    {
        changeWaveOnLoad = true;

        if (enemiesSpawnedBefore == totalEnemiesInTheLevel)
        {
            enemiesSpawned = enemiesSpawnedBefore;
            currentWave = currentWaveBefore;
            UpdateWaveLabel();
        }
        else
        {
            enemiesSpawned = 0;
            currentWave = 0;

            while (changeWaveOnLoad)
            {
                if (waveStaticData.Count != 0)
                {
                    if (enemiesSpawned < totalEnemiesInTheLevel)
                    {
                        currentWave++;
                        UpdateWaveLabel();
                        foreach (Enemy.EnemyType type in waveStaticData[currentWave - 1].types)
                        {
                            if (enemiesSpawnedBefore == 0 || enemiesSpawned > enemiesSpawnedBefore)
                            {
                                yield return new WaitForSeconds(spawnInterval);

                                Enemy enemy = EnemyFactory.Instance.CreateEnemy(type, enemySpawnPoint.position, gruntGolemPrefab.transform.rotation, wayPointsContainer);   // Using gruntGolemPrefab rotation for all since technically they all would have same rotation on start.
                            }
                            enemiesSpawned++;
                        }

                        if (enemiesSpawned > enemiesSpawnedBefore)
                            yield return new WaitForSeconds(waveInterval);
                    }
                    else
                    {
                        changeWaveOnLoad = false;
                    }
                }
            }
        }
    }

    private void SpawnExistingEnemyOnLoad(Enemy.EnemyType type, Vector3 pos, Quaternion rot, int health, Enemy.EnemyState state)
    {
        Enemy enemy = EnemyFactory.Instance.CreateEnemy(type, pos, rot, wayPointsContainer);
        enemy.SetEnemyState(state);
        enemy.healthDisplay.SetHealthValue(health);
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

    public void OnSave(string saveName)
    {
        this.current.playerData.health = playerHpBarController.CurrentHealthValue;
        this.current.playerData.wave = currentWave;
        this.current.playerData.score = score;
        this.current.playerData.totalEnemiesInTheLevel = totalEnemiesInTheLevel;
        this.current.playerData.totalEnemiesDead = totalEnemiesDead;
        this.current.playerData.enemiesKilled = enemiesKilled;
        this.current.playerData.enemiesSpawned = enemiesSpawned;
        this.current.playerData.towerPlaced = towersPlaced;
        this.current.playerData.gold = InventoryManager.instance.goldOnHand;
        this.current.playerData.stone = InventoryManager.instance.stoneOnHand;
        this.current.playerData.wood = InventoryManager.instance.woodOnHand;

        SerializationController.Save(saveName, this.current);
    }

    public void PreLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/saves/Save 1.save"))
        {
            SaveData.current = (SaveData)SerializationController.Load(Application.persistentDataPath + "/saves/Save 1.save");

            saveDataButton1.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Save 1";
            saveDataButton1.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Wave: " + SaveData.current.playerData.wave;
            saveDataButton1.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Player Health: " + SaveData.current.playerData.health;
            saveDataButton1.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Score: " + SaveData.current.playerData.score;

            loadDataButton1.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Save 1";
            loadDataButton1.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Wave: " + SaveData.current.playerData.wave;
            loadDataButton1.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Player Health: " + SaveData.current.playerData.health;
            loadDataButton1.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Score: " + SaveData.current.playerData.score;
        }

        if (File.Exists(Application.persistentDataPath + "/saves/Save 2.save"))
        {
            SaveData.current = (SaveData)SerializationController.Load(Application.persistentDataPath + "/saves/Save 2.save");

            saveDataButton2.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Save 1";
            saveDataButton2.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Wave: " + SaveData.current.playerData.wave;
            saveDataButton2.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Player Health: " + SaveData.current.playerData.health;
            saveDataButton2.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Score: " + SaveData.current.playerData.score;

            loadDataButton2.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Save 2";
            loadDataButton2.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Player Health: " + SaveData.current.playerData.health;
            loadDataButton2.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Wave: " + SaveData.current.playerData.wave;
            loadDataButton2.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Score: " + SaveData.current.playerData.score;
        }

    }

    public void OnLoad(string saveName)
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);

        ProjectileFactory.Instance.ReturnAllProjectiles();
        EnemyFactory.Instance.ReturnAllEnemies();
        TowerFactory.Instance.ReturnAllTowers();

        SaveData.current = (SaveData)SerializationController.Load(Application.persistentDataPath + "/saves/" + saveName);

        for (int i = 0; i < SaveData.current.towers.Count; i++)
        {
            TowerData currentTower = SaveData.current.towers[i];
            StartCoroutine(placer.PlaceTowerOnLoad(currentTower));
        }

        for (int i = 0; i < SaveData.current.enemies.Count; i++)
        {
            EnemyData currentEnemy = SaveData.current.enemies[i];
            SpawnExistingEnemyOnLoad(currentEnemy.enemyType, currentEnemy.enemyPosition, currentEnemy.enemyRotation, currentEnemy.health, currentEnemy.enemyState);
        }

        PlayerHealthBarController.instance.currentPlayerHealthValue = SaveData.current.playerData.health;

        towersPlaced = SaveData.current.playerData.towerPlaced;

        score = SaveData.current.playerData.score;
        totalEnemiesInTheLevel = SaveData.current.playerData.totalEnemiesInTheLevel;
        totalEnemiesDead = SaveData.current.playerData.totalEnemiesDead;
        enemiesKilled = SaveData.current.playerData.totalEnemiesDead;
        // enemiesSpawned = SaveData.current.playerData.enemiesSpawned; // update using the spawn coroutine instead
        // currentWave = SaveData.current.playerData.wave; // update using the spawn coroutine instead

        InventoryManager.instance.goldOnHand = SaveData.current.playerData.gold;
        InventoryManager.instance.stoneOnHand = SaveData.current.playerData.stone;
        InventoryManager.instance.woodOnHand = SaveData.current.playerData.wood;
        InventoryManager.instance.UpdateDisplay();

        spawnCoroutine = StartCoroutine(Spawn(SaveData.current.playerData.enemiesSpawned, SaveData.current.playerData.wave));
    }
}
