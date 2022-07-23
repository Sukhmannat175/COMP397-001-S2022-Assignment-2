/*  Filename:           PlayerProfile.cs
 *  Author:             Sukhmannat Singh (301168420)
 *  Last Update:        June 26, 2022
 *  Description:        PlayerProfile for save functionality
 *  Revision History:   June 26, 2022 (Sukhmannat Singh): Initial script.
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
    public float health;
    public Tower.TowerType towerType;
    public Vector3 towerPosition;
    public Quaternion towerRotation;
}

[System.Serializable]
public class EnemyData
{
    public string enemyId;
    public float health;
    public Enemy.EnemyType enemyType;
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
}