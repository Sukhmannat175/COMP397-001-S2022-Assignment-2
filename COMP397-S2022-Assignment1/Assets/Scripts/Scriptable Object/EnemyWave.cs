/*  Filename:           EnemyWave.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 26, 2022
 *  Description:        Enemy wave class, for save functionality
 *  Revision History:   June 26, 2022 (Sukhmannat Singh): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public List<Enemy.EnemyType> types;
}
