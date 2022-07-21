/*  Filename:           Cannonball.cs
 *  Author:             Ikamjot Hundal (301134374)
 *  Last Update:        June 26, 2022
 *  Description:        Use for CannonTower's projectiles.
 *  Revision History:   June 26, 2022 (Ikamjot Hundal): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : Projectile
{
    [SerializeField] float projectileSpeed = 10;
    [SerializeField] float cannonBallRange = 2f;
    Transform targetTransform;
    Vector3 enemyPos;

    [SerializeField] bool targetGone = false;
    [SerializeField] AudioClip enemyDeathSound;
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponentInParent<CannonTower>().GetFirstEnemy();
        enemyPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
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

        if (Vector3.Distance(transform.position, enemyPos) < 0.001f)
        {

            SplashDamageOccur();
            Destroy(gameObject);
        }
    }

    private void SplashDamageOccur()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, cannonBallRange);
        foreach (Collider collider in colliders)
        {

            if (collider.gameObject.CompareTag("Enemy"))
            {
                //  StartSplashDamage();
                GameObject enemy = collider.gameObject;

                enemy.GetComponent<EnemyBaseBehaviour>().TakeEnemyDamage(damage);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            SoundManager.instance.PlaySFX(enemyDeathSound);
            SplashDamageOccur();
        }
    }

   /* public void StartSplashDamage()
    {
        StartCoroutine(SplashSequence());
    } */

    private IEnumerator SplashSequence()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        //Draw the suspension
        Gizmos.DrawLine(
            Vector3.zero,
            Vector3.up
        );

        //draw force application point
        Gizmos.DrawWireSphere(Vector3.zero, 0.5f);

        Gizmos.color = Color.white;
    }

}
