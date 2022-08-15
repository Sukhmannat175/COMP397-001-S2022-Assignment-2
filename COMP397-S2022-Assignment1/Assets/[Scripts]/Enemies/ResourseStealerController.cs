/*  Filename:           ResourseSteaerController.cs
 *  Author:             Sukhmannat Singh (301168420)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        August 1, 2022
 *  Description:        Inventory Manager.
 *  Revision History:   June 26, 2022 (Sukhmannat Singh): Initial script.
 *                      June 26, 2022 (Sukhmannat Singh): Added logic to add data to save file
 *                      August 1, 2022 (Yuk Yee Wong): Reorganised the code and adapted object pooling.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseStealerController : EnemyBaseBehaviour
{
    [Header("Player resource decrement per steal")]
    [SerializeField] private int gold;
    [SerializeField] private int stone;
    [SerializeField] private int wood;

    [Header("Animation Settings")]
    [SerializeField] private string animationStateParameterName;
    [SerializeField] private int walkAnimationState;
    [SerializeField] private int digAnimationState;

    [Header("Other Settings")]
    [SerializeField] private int stealTime;
    [SerializeField] private int actionDelay; // preparing to dig
    [SerializeField] private int actionTime; // digging
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    [Header("Debug")]
    [SerializeField] private int remainingStealTimes;

    private Animator animator;
    private Rigidbody rb;
    
    private Coroutine digCoroutine;
    private Coroutine stealCoroutine;

    public IEnumerator Dig()
    {
        state = EnemyState.WALK;

        while (true)
        {
            if (state == EnemyState.WALK)
            {
                yield return new WaitForSeconds(actionDelay);
                navMeshAgent.speed = 0;
                animator.SetInteger(animationStateParameterName, digAnimationState);
                state = EnemyState.DIG;
            }
            yield return null;
        }
    }

    public IEnumerator StealResources()
    {
        while (true)
        {
            remainingStealTimes = stealTime;
            while (remainingStealTimes > 0 && state == EnemyState.DIG)
            {
                yield return new WaitForSeconds(actionTime);

                navMeshAgent.speed = speed;
                InventoryManager.instance.DecreaseResources(gold, stone, wood);
                remainingStealTimes--;
                if (remainingStealTimes == 1)
                {
                    state = EnemyState.WALK;
                }
            }

            yield return null;
        }
    }

    protected override string idPrefix { get { return "ResourceStealer"; } }

    protected override EnemyType enemyType { get { return EnemyType.RESOURCESTEALER; } }

    public override void EnemyStartBehaviour()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public override void EnemyOnEnableBehaviour()
    {
        if (digCoroutine != null)
        {
            StopCoroutine(digCoroutine);
        }

        if (stealCoroutine != null)
        {
            StopCoroutine(stealCoroutine);
        }

        if (wayPoints.Count > path)
            Walk(wayPoints[path]);


        if (gameObject.activeInHierarchy)
        {
            digCoroutine = StartCoroutine(Dig());
            stealCoroutine = StartCoroutine(StealResources());
        }
    }

    public override void EnemyUpdateBehaviour()
    {
        base.EnemyUpdateBehaviour();

        switch (state)
        {
            case EnemyState.WALK:
                meshRenderer.enabled = true;
                animator.SetInteger(animationStateParameterName, walkAnimationState);
                rb.detectCollisions = true;
                Walk(wayPoints[path]);
                break;

            case EnemyState.DIG:
                navMeshAgent.speed = speed;
                meshRenderer.enabled = false;
                rb.detectCollisions = false;
                CheckPathEnd();
                break;

            default:
                Debug.Log(state + " does not support by code.");
                break;
        }
    }

    // Without using walk method in dig state, the path number will not increase
    // and the enemy cannot damage player at the end of the path
    private void CheckPathEnd()
    {
        if (wayPoints.Count > 0
            && Vector3.Distance(transform.position, wayPoints[wayPoints.Count - 1].position) < 1f)
        {
            PathEnd();
        }
    }

    protected override void ReturnToPool()
    {
        EnemyFactory.Instance.ReturnPooledResourceStealer(this);
    }
}
