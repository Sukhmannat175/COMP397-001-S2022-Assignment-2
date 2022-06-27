/*  Filename:           TowerDestroyerController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 18, 2022
 *  Description:        Controls tower destroyer.
 *  Revision History:   June 18, 2022 (Yuk Yee Wong): Initial script.
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
    [SerializeField] private EnemyState state;

    [HideInInspector] public EnemyData enemyData;

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

    public override void Intialize(EnemyStaticData data)
    {
        base.Intialize(data);
        damage = data.ap;

        projectile.Init(damage);
    }

    public override void EnemyStartBehaviour()
    {
        base.EnemyStartBehaviour();

        if (string.IsNullOrEmpty(enemyData.enemyId))
        {
            enemyData.enemyId = "TowerDestroyer" + Random.Range(0, int.MaxValue).ToString();
            enemyData.enemyType = EnemyType.TOWERDESTROYER;
            GameController.instance.current.enemies.Add(enemyData);
        }
    }
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

        enemyData.enemyPosition = transform.position;
        enemyData.enemyRotation = transform.rotation;
    }
}
