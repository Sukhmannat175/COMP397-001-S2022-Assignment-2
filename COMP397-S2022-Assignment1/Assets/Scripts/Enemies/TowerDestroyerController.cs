/*  Filename:           TowerDestroyerController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *                      Sukhmannat Singh (301168420)
 *  Last Update:        June 26, 2022
 *  Description:        Controls tower destroyer.
 *  Revision History:   June 18, 2022 (Yuk Yee Wong): Initial script.
 *                      June 26, 2022 (Sukhmannat Singh): Added logic to add data to save file.
 *                      June 26, 2022 (Yuk Yee Wong): Change EnemyStartBehavior method to Initialize.
 *                      Auguest 1, 2022 (Yuk Yee Wong): Reorganised the code and adapted object pooling.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Work with Enemy Projectile in the same prefab to damage tower
/// </summary>
public class TowerDestroyerController : EnemyBaseBehaviour
{
    [Header("Tower Destroyer")]
    [SerializeField] private EnemyProjectile projectile;
    [SerializeField] private int damage;
    [SerializeField] private Animator animator;
    [SerializeField] private string animationStateParameterName;
    [SerializeField] private int walkState;
    [SerializeField] private int attackState;

    [Header("Debug")]
    [SerializeField] private Tower target;

    public void SetTargetTower(Tower tower)
    {
        target = tower;

        if (target == null)
        {
            state = EnemyState.WALK;
        }    
        else
        {
            state = EnemyState.ATTACK;
        }
    }

    public override void Intialize(EnemyStaticData data, Transform wayPointsContainer)
    {
        base.Intialize(data, wayPointsContainer);
        damage = data.ap;
        projectile.Init(damage);
    }

    public override void EnemyStartBehaviour() { }

    public override void EnemyOnEnableBehaviour() { }

    protected override string idPrefix { get { return "TowerDestroyer"; } }

    protected override EnemyType enemyType { get { return EnemyType.STONEMONSTER; } }

    public override void EnemyUpdateBehaviour()
    {
        base.EnemyUpdateBehaviour();

        switch(state)
        {
            case EnemyState.WALK:
                animator.SetInteger(animationStateParameterName, walkState);
                Walk(wayPoints[path]);
                break;

            case EnemyState.ATTACK:
                if (target != null)
                {
                    animator.SetInteger(animationStateParameterName, attackState);
                    navMeshAgent.SetDestination(target.transform.position);
                }
                break;

            default:
                Debug.Log(state + " does not support by code.");
                break;
        }
    }

    protected override void ReturnToPool()
    {
        EnemyFactory.Instance.ReturnPooledStoneMonster(this);
    }
}
