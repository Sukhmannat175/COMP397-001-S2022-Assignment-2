/*  Filename:           CrossbowTower.cs
 *  Author:             Han Bi (301176547)
 *                      Sukhmannat Singh (301168420)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        June 26, 2022
 *  Description:        Use for crossbow tower projectiles.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.
 *                      June 26, 2022 (Sukhmannat Singh): Added logic to add data to save file
 *                      June 26, 2022 (Yuk Yee Wong): Set projectile damage in CrossbowTower.
 *                      August 1, 2022 (Yuk Yee Wong): Refactored the code to ShootingTower.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowTower : ShootingTower
{
    protected override string idPrefix {  get { return "CrossbowTower"; } }
    protected override TowerType towerType { get { return TowerType.CrossbowTower; } }

    public override int GetTowerType()
    {
        return (int)TowerType.CrossbowTower;
    }

    protected override Projectile GetProjectile(Vector3 spawnPosition, int damage, GameObject target)
    {
        if (ProjectileFactory.Instance != null)
            return ProjectileFactory.Instance.CreateArrow(spawnPosition, damage, target);
        else
            return null;
    }

    protected override void ReturnToPool()
    {
        TowerFactory.Instance.ReturnPooledCrossbowTower(this);
    }
}
