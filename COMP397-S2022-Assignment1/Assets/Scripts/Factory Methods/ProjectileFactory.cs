/*  Filename:           ProjectileFactory.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        August 1, 2022
 *  Description:        For creating projectile objects
 *  Revision History:   August 1, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    [SerializeField] private ProjectilePoolManager arrowPoolManager;
    [SerializeField] private ProjectilePoolManager cannonBallPoolManager;

    public static ProjectileFactory Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public Projectile CreateArrow(Vector3 spawnPosition, int damage, GameObject target)
    {
        return arrowPoolManager.GetPooledProjectile(spawnPosition, damage, target);
    }

    public Projectile CreateCannonBall(Vector3 spawnPosition, int damage, GameObject target)
    {
        return cannonBallPoolManager.GetPooledProjectile(spawnPosition, damage, target);
    }

    public void ReturnPooledArrow(Projectile arrow)
    {
        arrowPoolManager.ReturnPooledProjectile(arrow);
    }

    public void ReturnPooledCannonBall(Projectile cannonBall)
    {
        cannonBallPoolManager.ReturnPooledProjectile(cannonBall);
    }

    public void ReturnAllProjectiles()
    {
        arrowPoolManager.ReturnAllPooledObjects();
        cannonBallPoolManager.ReturnAllPooledObjects();
    }
}
