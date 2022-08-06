/*  Filename:           PlayerProfile.cs
 *  Author:             Sukhmannat Singh (301168420)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        August 1, 2022
 *  Description:        PlayerProfile for save functionality
 *  Revision History:   June 26, 2022 (Sukhmannat Singh): Initial script.
 *                      July 20, 2022 (Sukhmannat Singh): Added player data.
 *                      August 1, 2022 (Yuk Yee Wong): Added player data variables and TowerData variable.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml.Serialization;

[System.Serializable]
public class TowerData
{
    public string towerId;
    public bool isBuilding;
    public float buildingTime = 0;
    public float health;
    public Tower.TowerType towerType;
    public Vector3 towerPosition;
    public Quaternion towerRotation;
}

[System.Serializable]
public class EnemyData
{
    public string enemyId;
    public int health;
    public Enemy.EnemyType enemyType;
    public Enemy.EnemyState enemyState;
    public Vector3 enemyPosition;
    public Quaternion enemyRotation;
}

[System.Serializable]
public class PlayerData
{
    public int health;
    public int wave;
    public int score;
    public int gold;
    public int stone;
    public int wood;
    public int totalEnemiesInTheLevel = 0;
    public int totalEnemiesDead = 0;
    public int enemiesKilled = 0;
    public int enemiesSpawned = 0;
    public int towerPlaced = 0;
}