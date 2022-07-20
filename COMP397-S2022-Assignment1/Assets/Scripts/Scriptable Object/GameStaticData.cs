/*  Filename:           GameStaticData.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 26, 2022
 *  Description:        Game Statistics, for save functionality
 *  Revision History:   June 26, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStaticData", menuName = "ScriptableObjects/GameStaticData", order = 1)]
public class GameStaticData: ScriptableObject
{
    public float waveInterval;
    public float spawnInterval;
    public List<EnemyWave> waveStaticData;
    public List<EnemyStaticData> enemyStaticData;
}