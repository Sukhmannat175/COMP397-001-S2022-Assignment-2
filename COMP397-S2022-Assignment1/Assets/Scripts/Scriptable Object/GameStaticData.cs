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