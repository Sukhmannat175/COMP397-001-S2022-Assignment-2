/*  Filename:           ProjectilePoolManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Description:        For creating project object pooling
 *  Revision History:   Auguest 1, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class ProjectilePoolManager : PoolManager<Projectile>
{
    public Projectile GetPooledProjectile(Vector3 spawnPosition, int damage, GameObject target)
    {
        var newProjectile = GetPooledObject(spawnPosition, false); // avoid update when waypoint is null
        newProjectile.SetDamage(damage);
        newProjectile.SetTarget(target);
        newProjectile.gameObject.SetActive(true);
        return newProjectile;
    }

    public void ReturnPooledProjectile(Projectile returnedProjectile)
    {
        ReturnPooledObject(returnedProjectile.gameObject);
    }
}
