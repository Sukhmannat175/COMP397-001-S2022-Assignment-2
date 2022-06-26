using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TowerData
{
    private Tower tower;

    public string towerId;
    public Tower.TowerType towerType;
    public Vector3 towerPosition;
    public Quaternion towerRotation;
}

[System.Serializable]
public class EnemyData
{
    private Enemy enemy;

    public string enemyId;
    public Enemy.EnemyType enemyType;
    public Vector3 enemyPosition;
    public Quaternion enemyRotation;
}