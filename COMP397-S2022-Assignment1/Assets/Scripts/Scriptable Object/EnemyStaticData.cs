using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStaticData
{
    public Enemy.EnemyType enemy;
    public int hp;
    public int ap;
    public float speed;
    public int goldPerHead;
    public int scorePerHead;
    public float stoppingDistance;
}