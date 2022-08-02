/*  Filename:           EnemyFactory.cs
 *  Author:             Han Bi (301176547)
 *                      Marcus Ngooi (301147411)
 *                      Yuk Yee Wong (301234795)
 *  Description:        For creating enemy objects
 *  Revision History:   July 20, 2022 (Han Bi): Initial script.
 *                      July 24, 2022 (Marcus Ngooi): Added rotation as parameter to create functions.
 *                      Auguest 1, 2022 (Yuk Yee Wong): Reorganised the code and adapted object pooling.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private EnemyPoolManager gruntGolemPoolManager;
    [SerializeField] private EnemyPoolManager stoneMonsterPoolManager;
    [SerializeField] private EnemyPoolManager resourcesStealerPoolManager;

    [Header("Loaded from Resources")]
    [SerializeField] private EnemyStaticData gruntGolemStaticData;
    [SerializeField] private EnemyStaticData stoneMonsterStaticData;
    [SerializeField] private EnemyStaticData resourcesStealerStaticData;

    public static EnemyFactory Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GameStaticData gameStaticData = Resources.Load<GameStaticData>("ScriptableObjects/GameStaticData");
        if (gameStaticData != null)
        {
            gruntGolemStaticData = gameStaticData.enemyStaticData.Find(x => x.enemy == Enemy.EnemyType.GRUNTGOLEM);
            stoneMonsterStaticData = gameStaticData.enemyStaticData.Find(x => x.enemy == Enemy.EnemyType.STONEMONSTER);
            resourcesStealerStaticData = gameStaticData.enemyStaticData.Find(x => x.enemy == Enemy.EnemyType.RESOURCESTEALER);
        }
        else
        {
            Debug.LogError("gameStaticData cannot be loaded");
        }
    }

    public Enemy CreateEnemy(Enemy.EnemyType enemyType, Vector3 position, Quaternion rotation, Transform wayPointsContainer)
    {
        Enemy enemy;

        switch (enemyType)
        {
            case Enemy.EnemyType.GRUNTGOLEM:
                enemy = CreateGruntGolem(position, rotation, wayPointsContainer);
                break;

            case Enemy.EnemyType.STONEMONSTER:
                enemy = CreateStoneMonster(position, rotation, wayPointsContainer);
                break;

            case Enemy.EnemyType.RESOURCESTEALER:
                enemy = CreateResourceStealer(position, rotation, wayPointsContainer);
                break;

            default:
                Debug.LogError(enemyType + " is not yet defined in spawn method");
                enemy = null;
                break;
        }

        return enemy;
    }

    // helper functions and/or concrete creator functions
    private Enemy CreateGruntGolem(Vector3 position, Quaternion rotation, Transform wayPointsContainer)
    {
        return gruntGolemPoolManager.GetPooledEnemy(position, rotation, wayPointsContainer, gruntGolemStaticData);
    }

    private Enemy CreateStoneMonster(Vector3 position, Quaternion rotation, Transform wayPointsContainer)
    {
        return stoneMonsterPoolManager.GetPooledEnemy(position, rotation, wayPointsContainer, stoneMonsterStaticData);
    }

    private Enemy CreateResourceStealer(Vector3 position, Quaternion rotation, Transform wayPointsContainer)
    {
        return resourcesStealerPoolManager.GetPooledEnemy(position, rotation, wayPointsContainer, resourcesStealerStaticData);
    }

    public void ReturnPooledGruntGolem(Enemy gruntGolem)
    {
        gruntGolemPoolManager.ReturnPooledEnemy(gruntGolem);
    }

    public void ReturnPooledStoneMonster(Enemy stoneMonster)
    {
        stoneMonsterPoolManager.ReturnPooledEnemy(stoneMonster);
    }

    public void ReturnPooledResourceStealer(Enemy resourceStealer)
    {
        resourcesStealerPoolManager.ReturnPooledEnemy(resourceStealer);
    }

    public void ReturnAllEnemies()
    {
        gruntGolemPoolManager.ReturnAllPooledObjects();
        stoneMonsterPoolManager.ReturnAllPooledObjects();
        resourcesStealerPoolManager.ReturnAllPooledObjects();
    }
}
