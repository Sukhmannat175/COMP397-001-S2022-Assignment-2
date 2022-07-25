/*  Filename:           GameController.cs
 *  Author:             Sukhmannat Singh (301168420)
 *                      Yuk Yee Wong (301234795)
 *                      Han Bi (301176547)
 *                      Marcus Ngooi (301147411)
 *  Last Update:        June 26, 2022
 *  Description:        Controls aspects of the game including spawning enemy waves, ending the game, keeping track of vital statistics.
 *  Revision History:   June 11, 2022 (Sukhmannat Singh): Initial script.
 *                      June 26, 2022 (Yuk Yee Wong): Adding wave management scripts.
 *                      July 20, 2022 (Han Bi): Refactored code to work with EnemyFactory.cs.
 *                      July 22, 2022 (Sukhmannat Singh): Added Save/Load methods.
 *                      July 24, 2022 (Marcus Ngooi): Integrated Factory Design pattern with load system.
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
    [SerializeField] private Transform enemyContainer;

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
    [SerializeField] private GameObject towers;
    [SerializeField] private GameObject enemies;

    [Header("Debug")]
    [SerializeField] private int currentWave;
    [SerializeField] private int score = 0;
    [SerializeField] private int enemiesKilled = 0;
    [Tooltip("Include those killed by towers and self-destructed when reached the end of the path")]
    [SerializeField] private int totalEnemiesDead = 0;
    [SerializeField] private int totalEnemiesInTheLevel;

    public static GameController instance;    
    public SaveData current;
    private bool changeWaveOnLoad = true;
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
        while (changeWaveOnLoad)
        {
            if (currentWave < 10)
            {
                currentWave++;
                UpdateWaveLabel();
                foreach (Enemy.EnemyType type in waveStaticData[currentWave - 1].types)
                {
                    yield return new WaitForSeconds(spawnInterval);

                    Enemy enemy = null;

                    enemy = EnemyFactory.instance.CreateEnemy(type, enemySpawnPoint.position, gruntGolemPrefab.transform.rotation);   // Using gruntGolemPrefab rotation for all since technically they all would have same rotation on start.
                    enemy.transform.parent = enemyContainer;

                    if (enemy != null)
                        enemy.SetWayPoints(wayPointsContainer);
                }

                yield return new WaitForSeconds(waveInterval);
            }
            else
            {
                changeWaveOnLoad = false;
            }
        }
    }

    private void SpawnOnLoad(Enemy.EnemyType type, Vector3 pos, Quaternion rot, int health)
    {
        Enemy enemy;
        UpdateWaveLabel();

        enemy = EnemyFactory.instance.CreateEnemy(type, pos, rot);
        enemy.transform.parent = enemyContainer;
        enemy.SetWayPoints(wayPointsContainer);
        enemy.healthDisplay.SetHealthValue(health);
    }

    //private IEnumerator Spawn()
    //{
    //    while (changeWaveOnLoad)
    //    {
    //        if (currentWave < 10)
    //        {
    //            currentWave++;
    //            UpdateWaveLabel();
    //            foreach (Enemy.EnemyType type in waveStaticData[currentWave - 1].types)
    //            {
    //                yield return new WaitForSeconds(spawnInterval);

    //                Enemy enemy = null;

    //                switch (type)
    //                {
    //                    case Enemy.EnemyType.GRUNTGOLEM:
    //                        enemy = Instantiate(gruntGolemPrefab, enemySpawnPoint.position, gruntGolemPrefab.transform.rotation, enemyContainer);
    //                        enemy.Intialize(gruntGolemStaticData);
    //                        break;
    //                    case Enemy.EnemyType.STONEMONSTER:
    //                        enemy = Instantiate(stoneMonsterPrefab, enemySpawnPoint.position, stoneMonsterPrefab.transform.rotation, enemyContainer);
    //                        enemy.Intialize(stoneMonsterStaticData);
    //                        break;
    //                    case Enemy.EnemyType.RESOURCESTEALER:
    //                        enemy = Instantiate(resourcesStealerPrefab, enemySpawnPoint.position, resourcesStealerPrefab.transform.rotation, enemyContainer);
    //                        enemy.Intialize(resourcesStealerStaticData);
    //                        break;
    //                    default:
    //                        Debug.LogError(type + " is not yet defined in spawn method");
    //                        break;
    //                }

    //                if (enemy != null)
    //                    enemy.SetWayPoints(wayPointsContainer);
    //            }

    //            yield return new WaitForSeconds(waveInterval);
    //        }
    //        else
    //        {
    //            changeWaveOnLoad = false;
    //        }
    //    }
    //}

    //private void SpawnOnLoad(Enemy.EnemyType type, Vector3 pos, Quaternion rot, int health)
    //{
    //    Enemy enemy ;
    //    UpdateWaveLabel();

    //    switch (type)
    //    {
    //        case Enemy.EnemyType.GRUNTGOLEM:
    //            enemy = Instantiate(gruntGolemPrefab, pos, rot, enemyContainer);
    //            enemy.Intialize(gruntGolemStaticData);
    //            enemy.SetWayPoints(wayPointsContainer);
    //            enemy.healthDisplay.SetHealthValue(health);
    //            break;
    //        case Enemy.EnemyType.STONEMONSTER:
    //            enemy = Instantiate(stoneMonsterPrefab, pos, rot, enemyContainer);
    //            enemy.Intialize(stoneMonsterStaticData);
    //            enemy.SetWayPoints(wayPointsContainer);
    //            enemy.healthDisplay.SetHealthValue(health);
    //            break;
    //        case Enemy.EnemyType.RESOURCESTEALER:
    //            enemy = Instantiate(resourcesStealerPrefab, pos, rot, enemyContainer);
    //            enemy.Intialize(resourcesStealerStaticData);
    //            enemy.SetWayPoints(wayPointsContainer);
    //            enemy.healthDisplay.SetHealthValue(health);
    //            break;
    //        default:
    //            Debug.LogError(type + " is not yet defined in spawn method");
    //            break;
    //    }
    //}

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
        int towerCount = towers.transform.childCount;
        int enemyCount = enemies.transform.childCount;

        if (towers.transform.childCount > 0)
        {
            for (int i = 0; i < towerCount; i++)
            {
                Destroy(towers.transform.GetChild(i).gameObject);
            }
        }

        if (enemies.transform.childCount > 0)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Destroy(enemies.transform.GetChild(i).gameObject);
            }
        }

        SaveData.current = (SaveData)SerializationController.Load(Application.persistentDataPath + "/saves/" + saveName);

        for (int i = 0; i < SaveData.current.towers.Count; i++)
        {
            TowerData currentTower = SaveData.current.towers[i];
            StartCoroutine(placer.PlaceTowerOnLoad(currentTower.towerType, currentTower.towerPosition, currentTower.towerRotation, currentTower.isBuilding, currentTower.health));
        }

        for (int i = 0; i < SaveData.current.enemies.Count; i++)
        {
            EnemyData currentEnemy = SaveData.current.enemies[i];
            SpawnOnLoad(currentEnemy.enemyType, currentEnemy.enemyPosition, currentEnemy.enemyRotation, currentEnemy.health);
        }

        PlayerHealthBarController.instance.currentPlayerHealthValue = SaveData.current.playerData.health;
        currentWave = SaveData.current.playerData.wave;
        score = SaveData.current.playerData.score;
        InventoryManager.instance.goldOnHand = SaveData.current.playerData.gold;
        InventoryManager.instance.stoneOnHand = SaveData.current.playerData.stone;
        InventoryManager.instance.woodOnHand = SaveData.current.playerData.wood;
        UpdateWaveLabel();
        InventoryManager.instance.UpdateDisplay();
    }
}
