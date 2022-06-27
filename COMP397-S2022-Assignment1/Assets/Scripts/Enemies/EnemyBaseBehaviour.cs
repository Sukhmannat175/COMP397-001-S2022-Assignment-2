/*  Filename:           EnemyBaseBehaviour.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 18, 2022
 *  Description:        Abstract Enemy Base Behaviour Class for all enemies.
 *  Revision History:   June 18, 2022 (Yuk Yee Wong): Initial script extracted from GruntGolemController with modifications on health, projectile, enemy damage etc.
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider), typeof(NavMeshAgent))]
public abstract class EnemyBaseBehaviour : Enemy
{
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private int maxHealth;
    [SerializeField] private int goldPerHead;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private int enemyDamage = 1;
    [SerializeField] protected int playerDamage = 1;
    [SerializeField] private int scorePerEnemyKilled = 10;

    [Header("Debug")]
    [SerializeField] protected int path = 0;
    [Tooltip("Assigned from game controller")]
    [SerializeField] protected List<Transform> wayPoints;
    [Tooltip("Come from nav mesh agent, for calculating distance travelled")]
    [SerializeField] protected float speed;
    [SerializeField] protected float distanceTravelled;

    private string playerProjectileTag = "Projectile";

    protected NavMeshAgent navMeshAgent;

    protected void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void TakeEnemyDamage(int damage)
    {
        healthDisplay.TakeDamage(damage);

        if (healthDisplay.CurrentHealthValue == 0)
        {
            GameController.instance.KillEnemey(scorePerEnemyKilled);
            InventoryManager.instance.CollectResources(goldPerHead, 0, 0);
            SoundManager.instance.PlayEnemyDeathSfx();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerProjectileTag))
        {
            TakeEnemyDamage(enemyDamage);
        }
    }

    protected void UpdateEnemyHpBarRotation()
    {
        healthDisplay.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    protected void UpdateDistanceTravelled()
    {
        distanceTravelled += speed * Time.deltaTime;
    }

    public override void SetWayPoints(Transform wayPointsContainer)
    {
        wayPoints.Clear();

        foreach (Transform wayPoint in wayPointsContainer.transform)
            wayPoints.Add(wayPoint);
    }

    public override float GetDistanceTravelled()
    {
        return distanceTravelled;
    }

    public override void EnemyUpdateBehaviour()
    {
        UpdateEnemyHpBarRotation();
        UpdateDistanceTravelled();
    }

    public override void EnemyStartBehaviour()
    {
        // Moved to intialize
    }

    public override void Intialize(EnemyStaticData data)
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        maxHealth = data.hp;
        navMeshAgent.speed = data.speed;
        navMeshAgent.stoppingDistance = data.stoppingDistance;
        goldPerHead = data.goldPerHead;
        scorePerEnemyKilled = data.scorePerHead;
        playerDamage = data.ap;

        healthDisplay.Init(maxHealth);
        SetSpeed(navMeshAgent.speed);

        enemyStaticData = data;
    }

    public override void Walk(Transform position)
    {
        navMeshAgent.destination = position.position;
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    path += 1;
                    if (path == wayPoints.Count)
                    {
                        PlayerHealthBarController.instance.TakeDamage(playerDamage);
                        GameController.instance.AddTotalEnemiesDead();
                        SoundManager.instance.PlayPlayerDamageSfx();
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
