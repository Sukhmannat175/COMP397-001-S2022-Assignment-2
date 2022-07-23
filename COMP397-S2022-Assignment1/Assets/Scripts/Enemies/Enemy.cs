/*  Filename:           Enemy.cs
 *  Author:             Han Bi (301176547)
 *  Last Update:        June 20, 2022
 *  Description:        Abstract Enemy Class for all enemies.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script which currently has mechanics to allow proper tower targeting.
 *                      June 18, 2022 (Yuk Yee Wong): Add Enemy Type, State, Start, Update and 5 abstract methods.
 *                      June 20, 2022 (Yuk Yee Wong): Add enemy static data to control the intialization.
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public EnemyState enemyState;
    [SerializeField] public HealthDisplay healthDisplay;

    public enum EnemyType
    {
        NONE = 0,
        GRUNTGOLEM = 1,
        STONEMONSTER = 2,
        RESOURCESTEALER = 3
    }
    public enum EnemyState
    {
        WALK = 0,
        ATTACK = 1,
        DIG = 2,
    }

    protected EnemyStaticData enemyStaticData;
    [HideInInspector] public string id;

    private void Start()
    {
        EnemyStartBehaviour();
    }

    private void Update()
    {
        EnemyUpdateBehaviour();
    }

    public abstract void Intialize(EnemyStaticData data);

    public abstract void SetWayPoints(Transform wayPointsContainer);

    public abstract float GetDistanceTravelled();

    public abstract void EnemyStartBehaviour();

    public abstract void EnemyUpdateBehaviour();

    public abstract void Walk(Transform position);
}
