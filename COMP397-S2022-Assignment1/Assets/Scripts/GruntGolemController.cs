using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GruntGolemController : Enemy
{
    public Transform[] wayPoints = { };
    public HealthBarController healthBarController;

    private NavMeshAgent navMeshAgent;
    private int path = 0;
    private int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetSpeed(navMeshAgent.speed);
    }

    // Update is called once per frame
    void Update()
    {
        Walk(wayPoints[path]);

        UpdateDistanceTravelled();

        if (this.health == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Walk(Transform position)
    {
        navMeshAgent.destination = position.position;
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    this.path += 1;
                    if (path == 7)
                    {
                        healthBarController.TakeDamage(1);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }

    public void TakeEnemyDamage(int dmg)
    {
        health -= dmg;
    }
}
