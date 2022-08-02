/*  Filename:           CannonTower.cs
 *  Author:             Ikamjot Hundal (301134374)
 *                      Yuk Yee Wong (301234795)
 *                      Sukhmannat Singh (301168420)
 *  Last Update:        June 26, 2022
 *  Description:        Use for Cannonball tower projectiles.
 *  Revision History:   June 26, 2022 (Ikamjot Hundal): Initial script.
 *                      June 26, 2022 (Yuk Yee Wong): Setting projectile damage in CannonTower.
 *                      June 26, 2022 (Sukhmannat Singh): Added logic to add data to save file.
 *                      August 1, 2022 (Yuk Yee Wong): Refactored the code to ShootingTower.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : ShootingTower
{
    protected override string idPrefix { get { return "CannonTower"; } }

    protected override TowerType towerType { get { return TowerType.CannonTower; } }

    public override int GetTowerType()
    {
        return (int)TowerType.CannonTower;
    }

    protected override Projectile GetProjectile(Vector3 spawnPosition, int damage, GameObject target)
    {
        if (ProjectileFactory.Instance != null)
            return ProjectileFactory.Instance.CreateCannonBall(spawnPosition, damage, target);
        else
            return null;
    }

    protected override void ReturnToPool()
    {
        TowerFactory.Instance.ReturnPooledCannonTower(this);
    }
}
