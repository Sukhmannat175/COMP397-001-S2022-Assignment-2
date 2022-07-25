/*  Filename:           EnemyFactory.cs
 *  Author:             Han Bi (301176547)
 *                      Marcus Ngooi (301147411)
 *  Description:        For creating enemy objects
 *  Revision History:   July 20, 2022 (Han Bi): Initial script.
 *                      July 24, 2022 (Marcus Ngooi): Added rotation as parameter to create functions.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private Enemy gruntGolemPrefab;
    [SerializeField] private Enemy stoneMonsterPrefab;
    [SerializeField] private Enemy resourcesStealerPrefab;

    [Header("Loaded from Resources")]
    [SerializeField] private EnemyStaticData gruntGolemStaticData;
    [SerializeField] private EnemyStaticData stoneMonsterStaticData;
    [SerializeField] private EnemyStaticData resourcesStealerStaticData;

    public static EnemyFactory instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
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

    public Enemy CreateEnemy(Enemy.EnemyType enemyType, Vector3 position, Quaternion rotation)
    {
        Enemy enemy;

        switch (enemyType)
        {
            case Enemy.EnemyType.GRUNTGOLEM:
                enemy = CreateGruntGolem(position, rotation);
                break;

            case Enemy.EnemyType.STONEMONSTER:
                enemy = CreateStoneMonster(position, rotation);
                break;

            case Enemy.EnemyType.RESOURCESTEALER:
                enemy = CreateResourceStealer(position, rotation);
                break;

            default:
                Debug.LogError(enemyType + " is not yet defined in spawn method");
                enemy = null;
                break;
        }

        return enemy;
    }

    // helper functions and/or concrete creator functions
    private Enemy CreateGruntGolem(Vector3 position, Quaternion rotation)
    {
        Enemy enemy = Instantiate(gruntGolemPrefab, position, rotation);
        enemy.Intialize(gruntGolemStaticData);

        return enemy;
    }

    private Enemy CreateStoneMonster(Vector3 position, Quaternion rotation)
    {
        Enemy enemy = Instantiate(stoneMonsterPrefab, position, rotation);
        enemy.Intialize(stoneMonsterStaticData);

        return enemy;
    }

    private Enemy CreateResourceStealer(Vector3 position, Quaternion rotation)
    {
        Enemy enemy = Instantiate(resourcesStealerPrefab, position, rotation);
        enemy.Intialize(resourcesStealerStaticData);

        return enemy;
    }

    //public Enemy CreateEnemy(Enemy.EnemyType enemyType, Vector3 position)
    //{
    //    Enemy enemy;

    //    switch (enemyType)
    //    {
    //        case Enemy.EnemyType.GRUNTGOLEM:
    //            enemy = CreateGruntGolem(position);
    //            break;

    //        case Enemy.EnemyType.STONEMONSTER:
    //            enemy = CreateStoneMonster(position);
    //            break;

    //        case Enemy.EnemyType.RESOURCESTEALER:
    //            enemy = CreateResourceStealer(position);
    //            break;

    //        default:
    //            Debug.LogError(enemyType + " is not yet defined in spawn method");
    //            enemy = null;
    //            break;
    //    }

    //    return enemy;
    //}

    //// helper functions and/or concrete creator functions
    //private Enemy CreateGruntGolem(Vector3 position)
    //{
    //    Enemy enemy = Instantiate(gruntGolemPrefab, position, gruntGolemPrefab.transform.rotation);
    //    enemy.Intialize(gruntGolemStaticData);

    //    return enemy;
    //}

    //private Enemy CreateStoneMonster(Vector3 position)
    //{
    //    Enemy enemy = Instantiate(stoneMonsterPrefab, position, gruntGolemPrefab.transform.rotation);
    //    enemy.Intialize(stoneMonsterStaticData);

    //    return enemy;
    //}

    //private Enemy CreateResourceStealer(Vector3 position)
    //{
    //    Enemy enemy = Instantiate(resourcesStealerPrefab, position, gruntGolemPrefab.transform.rotation);
    //    enemy.Intialize(resourcesStealerStaticData);

    //    return enemy;
    //}
}
