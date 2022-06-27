/*  Filename:           EnemyStaticData.cs
 *  Author:             Sukhmannat Singh (301168420)
 *  Last Update:        June 26, 2022
 *  Description:        Enemy Data, for save functionality
 *  Revision History:   June 26, 2022 (Sukhmannat Singh): Initial script.
 */


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