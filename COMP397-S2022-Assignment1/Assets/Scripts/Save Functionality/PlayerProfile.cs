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