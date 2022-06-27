/*  Filename:           TowerStaticData.cs
 *  Author:             Sukhmannat Singh (301168420)
 *  Last Update:        June 26, 2022
 *  Description:        TowerStaticData, for save functionality
 *  Revision History:   June 26, 2022 (Sukhmannat Singh): Initial script.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerStaticData
{
    public Tower.TowerType tower;
    public int hp;
    public int buildTime;
    public float interval;
    public int damageToEnemy;
}
