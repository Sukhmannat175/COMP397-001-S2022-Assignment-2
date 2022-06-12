using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GruntGolemController : Enemy
{
    public Transform[] wayPoints = { };
    public HealthBarController healthBarController;
    public Transform child;

    private NavMeshAgent navMeshAgent;
    private int path = 0;
    private int health = 5;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip playerSound;

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
        child.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        
        UpdateDistanceTravelled();
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
                        GameController.instance.totalEnemiesDead += 1;
                        SoundManager.instance.PlaySFX(playerSound);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }

    public void TakeEnemyDamage(int dmg)
    {
        health -= dmg;
        if (health == 0)
        {
            GameController.instance.score += 10;
            GameController.instance.enemiesKilled += 1;
            GameController.instance.totalEnemiesDead += 1;
            SoundManager.instance.PlaySFX(deathSound);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {            
            TakeEnemyDamage(1);
        }
    }
}
