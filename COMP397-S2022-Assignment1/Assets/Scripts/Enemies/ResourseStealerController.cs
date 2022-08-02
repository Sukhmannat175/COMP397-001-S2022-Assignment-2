/*  Filename:           ResourseSteaerController.cs
 *  Author:             Sukhmannat Singh (301168420)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        June 24, 2022
 *  Description:        Inventory Manager.
 *  Revision History:   June 26, 2022 (Sukhmannat Singh): Initial script.
 *                      June 26, 2022 (Sukhmannat Singh): Added logic to add data to save file
 *                      Auguest 1, 2022 (Yuk Yee Wong): Reorganised the code and adapted object pooling.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseStealerController : EnemyBaseBehaviour
{
    [Header("Resource Stealer")]
    [SerializeField] private int gold;
    [SerializeField] private int stone;
    [SerializeField] private int wood;
    [SerializeField] private string animationStateParameterName;
    [SerializeField] private int walkState;
    [SerializeField] private int digState;
    [SerializeField] private int actionDelay;
    [SerializeField] private int stealTime;
    [SerializeField] private int actionTime;

    private Animator animator;
    private int steal;

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
                animator.SetInteger(animationStateParameterName, digState);
                yield return new WaitForSeconds(2);
                state = EnemyState.DIG;
            }
            yield return null;
        }
    }

    public IEnumerator StealResources()
    {
        while (true)
        {
            steal = stealTime;
            while (steal > 0 && state == EnemyState.DIG)
            {
                yield return new WaitForSeconds(actionTime);

                navMeshAgent.speed = speed;
                InventoryManager.instance.DecreaseResources(gold, stone, wood);
                steal--;
                if (steal == 1)
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
    }

    public override void EnemyOnEnableBehaviour() 
    {
        if (digCoroutine != null)
        {
            StopCoroutine(digCoroutine);
        }

        digCoroutine = StartCoroutine(Dig());

        if (stealCoroutine != null)
        {
            StopCoroutine(stealCoroutine);
        }

        stealCoroutine = StartCoroutine(StealResources());
    }

    public override void EnemyUpdateBehaviour()
    {
        base.EnemyUpdateBehaviour();

        switch (state)
        {
            case EnemyState.WALK:
                
                this.gameObject.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                animator.SetInteger(animationStateParameterName, walkState);
                Walk(wayPoints[path]);
                
                break;

            case EnemyState.DIG:
                navMeshAgent.speed = speed;
                this.gameObject.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
                
                break;

            default:
                Debug.Log(state + " does not support by code.");
                break;
        }

        enemyData.health = healthDisplay.CurrentHealthValue;
        enemyData.enemyPosition = transform.position;
        enemyData.enemyRotation = transform.rotation;
    }

    public void DigDown()
    {

        //this.gameObject.SetActive(false);
    }

    protected override void ReturnToPool()
    {
        EnemyFactory.Instance.ReturnPooledResourceStealer(this);
    }
}
