/*  Filename:           Cannonball.cs
 *  Author:             Ikamjot Hundal (301134374)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        August 1, 2022
 *  Description:        Use for CannonTower's projectiles.
 *  Revision History:   June 26, 2022 (Ikamjot Hundal): Initial script.
 *                      June 26, 2022 (Yuk Yee Wong): Remove the damage float to Projectile.
 *                      August 1, 2022 (Yuk Yee Wong): Reorganised the code and adapted object pooling.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : ProjectileBehaviour
{
    [SerializeField] private float cannonBallRange = 2f;
    protected override void OnHit()
    {
        SplashDamageOccur();

        ReturnToPool();
    }

    protected override void ReturnToPool()
    {
        // Object pooling
        if (ProjectileFactory.Instance != null)
            ProjectileFactory.Instance.ReturnPooledCannonBall(this);
        else
            Destroy(gameObject);
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

    /* public void StartSplashDamage()
     {
         StartCoroutine(SplashSequence());
     }

     private IEnumerator SplashSequence()
     {
         yield return new WaitForSeconds(1f);
         Destroy(gameObject);
     }*/

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
