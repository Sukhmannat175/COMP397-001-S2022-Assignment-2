using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml.Serialization;

[System.Serializable]
public class TowerData
{
    public string towerId;
    public Tower.TowerType towerType;
    public Vector3 towerPosition;
    public Quaternion towerRotation;
}

[System.Serializable]
public class EnemyData
{
    public string enemyId;
    public Enemy.EnemyType enemyType;
    public Vector3 enemyPosition;
    public Quaternion enemyRotation;
}