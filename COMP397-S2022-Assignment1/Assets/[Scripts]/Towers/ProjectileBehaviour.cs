/*  Filename:           ProjectileBehaviour.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        August 1, 2022
 *  Description:        Abstract class for projectile behaviour.
 *  Revision History:   August 1, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ProjectileBehaviour : Projectile
{
    [SerializeField] private float projectileSpeed = 10;
    [SerializeField] protected bool targetGone = false;
    [SerializeField] protected AudioClip enemyDeathSound;
    [SerializeField] protected float enemyDistanceOffset = 0.001f;
    [SerializeField] protected float maxMagnitudeDelta = 0.01f;
    private Vector3 enemyPos;

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
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, maxMagnitudeDelta);
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.position = Vector3.MoveTowards(transform.position, enemyPos, step);

        if (Vector3.Distance(transform.position, enemyPos) < enemyDistanceOffset)
        {
            OnHit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            SoundManager.instance.PlaySFX(enemyDeathSound);
            OnHit();
        }
        else if (other.CompareTag("Ground"))
        {
            ReturnToPool();
        }
    }
}
