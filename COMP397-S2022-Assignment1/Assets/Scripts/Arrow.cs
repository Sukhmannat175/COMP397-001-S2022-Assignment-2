/*
 *Created by: Han Bi 301176547
 *Child class of the Projectile class
 *Use for arrow tower projectiles
 *Last update: June 7, 2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : Projectile
{
    [SerializeField] float damage = 1;
    [SerializeField] float projectileSpeed = 20;
    Transform targetTransform;
    Vector3 enemyPos;

    [SerializeField] bool targetGone = false;
    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] GruntGolemController gruntGolemController;
    // Start is called before the first frame update
    void Start()
    {

        target = GetComponentInParent<CrossbowTower>().GetFirstEnemy();
        enemyPos = target.transform.position;


    }

    private void Update()
    {
        float step = projectileSpeed * Time.deltaTime;


        if (!targetGone)
        {
            try
            {
                enemyPos = target.transform.position;
            }
            catch
            {
                targetGone = true;
            }
        }

        Vector3 targetDirection = enemyPos - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.01f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.position = Vector3.MoveTowards(transform.position, enemyPos, step);

        if (Vector3.Distance(transform.position, enemyPos)< 0.001f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            SoundManager.instance.PlaySFX(enemyDeathSound);
            gruntGolemController.TakeEnemyDamage(1);
            Destroy(gameObject);
        }
    }

}
