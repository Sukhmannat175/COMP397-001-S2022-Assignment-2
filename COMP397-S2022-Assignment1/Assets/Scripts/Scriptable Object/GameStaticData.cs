/*  Filename:           GameStaticData.cs
 *  Author:             Sukhmannat Singh (301168420)
 *  Last Update:        June 26, 2022
 *  Description:        Game Statistics, for save functionality
 *  Revision History:   June 26, 2022 (Sukhmannat Singh): Initial script.
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