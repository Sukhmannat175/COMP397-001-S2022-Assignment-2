/*  Filename:           EnemyStaticData.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 26, 2022
 *  Description:        Enemy Data, for save functionality
 *  Revision History:   June 26, 2022 (Yuk Yee Wong): Initial script.
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