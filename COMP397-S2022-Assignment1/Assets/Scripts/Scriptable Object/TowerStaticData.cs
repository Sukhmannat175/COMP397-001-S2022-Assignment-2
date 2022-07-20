/*  Filename:           TowerStaticData.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 26, 2022
 *  Description:        TowerStaticData, for save functionality
 *  Revision History:   June 26, 2022 (Yuk Yee Wong): Initial script.
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
