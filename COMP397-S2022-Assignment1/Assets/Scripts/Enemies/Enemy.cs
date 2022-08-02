/*  Filename:           Enemy.cs
 *  Author:             Han Bi (301176547)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        June 20, 2022
 *  Description:        Abstract Enemy Class for all enemies.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script which currently has mechanics to allow proper tower targeting.
 *                      June 18, 2022 (Yuk Yee Wong): Added Enemy Type, State, Start, Update and 5 abstract methods.
 *                      June 20, 2022 (Yuk Yee Wong): Added enemy static data to control the intialization.
 *                      August 1, 2022 (Yuk Yee Wong): Added new abstract methods.
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
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

    [Tooltip("Updated by itself")]
    [SerializeField] protected EnemyState state;

    [HideInInspector] public EnemyData enemyData;
    [HideInInspector] public string id;

    private void Start()
    {
        EnemyStartBehaviour();
    }

    private void Update()
    {
        EnemyUpdateBehaviour();
    }

    private void OnEnable()
    {
        EnemyOnEnableBehaviour();
    }

    public abstract void Intialize(EnemyStaticData data, Transform wayPointsContainer);

    protected abstract void RefreshEnemyData();

    public void SetEnemyState(EnemyState state) { this.state = state; }

    public abstract float GetDistanceTravelled();

    protected abstract EnemyType enemyType { get; }

    protected abstract string idPrefix { get; }

    public abstract void EnemyStartBehaviour();

    public abstract void EnemyUpdateBehaviour();

    public abstract void EnemyOnEnableBehaviour();

    public abstract void Walk(Transform position);

    protected abstract void ReturnToPool();
}
