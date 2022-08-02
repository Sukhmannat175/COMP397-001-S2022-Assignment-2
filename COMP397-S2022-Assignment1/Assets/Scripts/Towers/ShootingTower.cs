/*  Filename:           ShootingTower.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        August 1, 2022
 *  Description:        Use for crossbow tower projectiles.
 *  Revision History:   August 1, 2022 (Yuk Yee Wong): Initial script - refactored the code from CannonTower and CrossTower to ShootingTower.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingTower : Tower
{
    [SerializeField]
    [Tooltip("Tower Range")]
    protected GameObject attackZone;

    [SerializeField]
    [Tooltip("Prefab of projectile this tower will shoot")]
    protected GameObject projectile;

    [SerializeField]
    [Tooltip("The location the Projectile will shoot from")]
    protected GameObject projectileSpawn;

    [SerializeField]
    [Tooltip("Audio Source for shooting")]
    protected AudioClip shootSound;

    [Header("Target")]
    [SerializeField] protected List<GameObject> targets = new List<GameObject>();
    [SerializeField] protected GameObject currentTarget = null;

    protected abstract Projectile GetProjectile(Vector3 spawnPosition, int damage, GameObject target);

    protected override void ResetTower()
    {
        targets.Clear();
        currentTarget = null;
        base.ResetTower();
    }

    protected override void TowerStartBehaviour()
    {
        targets = new List<GameObject>();
        base.TowerStartBehaviour();
    }

    protected override void TowerUpdateBehaviour()
    {
        UpdateCurrentTarget();

        if (coolingDown == false && currentTarget != null)
        {
            StartCoroutine(Shoot());
        }
        towerData.isBuilding = GetIsBuilding();
        towerData.health = health.currentHealth;
    }


    private IEnumerator Shoot()
    {
        coolingDown = true; //stops the coroutine from being called again
        SoundManager.instance.PlaySFX(shootSound);

        GetProjectile(projectileSpawn.transform.position, damageToEnemy, currentTarget);

        yield return new WaitForSeconds(actionDelay);
        coolingDown = false; // releases the coroutine to be called
    }

    public GameObject GetFirstEnemy()
    {
        GameObject firstEnemy;

        //if some enemies get unexpectedly destroyed while in range, remove them from list
        if (targets.Count > 0)
        {
            for (int i = targets.Count - 1; i >= 0; i--)
            {
                if (targets[i] == null || !targets[i].activeInHierarchy)
                {
                    targets.Remove(targets[i]);
                }
            }
        }

        if (targets.Count == 0)
        {
            try
            {
                return targets[0];
            }
            catch
            {
                return null;
            }
        }
        else
        {
            firstEnemy = targets[0];
            for (int i = 1; i < targets.Count; i++)
            {
                if (targets[i].GetComponent<Enemy>().GetDistanceTravelled() > firstEnemy.GetComponent<Enemy>().GetDistanceTravelled())
                {
                    firstEnemy = targets[i];
                }
            }
        }

        return firstEnemy;
    }

    public override void AddToTargets(GameObject gameObject)
    {
        if (!targets.Contains(gameObject))
        {
            targets.Add(gameObject);
            UpdateCurrentTarget();
        }
    }

    public override void RemoveFromTargets(GameObject gameObject)
    {
        targets.Remove(gameObject);
        UpdateCurrentTarget();
    }

    public void UpdateCurrentTarget() //sets currentTarget to first Target in array
    {
        currentTarget = GetFirstEnemy();
    }
}
