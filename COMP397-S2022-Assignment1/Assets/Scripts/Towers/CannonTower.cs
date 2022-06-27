/*  Filename:           CannonTower.cs
 *  Author:             Ikamjot Hundal (301134374)
 *  Last Update:        June 26, 2022
 *  Description:        Use for Cannonball tower projectiles.
 *  Revision History:   June 26, 2022 (Ikamjot Hundal): Initial script.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
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
    AudioClip shootSound;

    private List<GameObject> targets = new List<GameObject>();
    [HideInInspector] public TowerData towerData;

    protected GameObject currentTarget = null;

    //for testing
    [SerializeField] private bool coolingDown = false; //used to flag tower cooldown in Coroutine

    protected override void TowerStartBehaviour()
    {
        targets = new List<GameObject>();

        id = "CannonTower" + Random.Range(0, int.MaxValue).ToString();

        if (string.IsNullOrEmpty(towerData.towerId))
        {
            towerData.towerId = id;
            towerData.towerType = TowerType.CannonTower;
            GameController.instance.current.towers.Add(towerData);
        }
    }

    protected override void TowerUpdateBehaviour()
    {
        UpdateCurrentTarget();

        if (coolingDown == false && currentTarget != null)
        {
            StartCoroutine(Shoot());
        }

        towerData.towerPosition = transform.position;
        towerData.towerRotation = transform.rotation;
    }

    private IEnumerator Shoot()
    {

        coolingDown = true; //stops the coroutine from being called again
        SoundManager.instance.PlaySFX(shootSound);
        ShootProjectile(currentTarget);

        yield return new WaitForSeconds(actionDelay);
        coolingDown = false; // releases the coroutine to be called

    }

    private void ShootProjectile(GameObject target)
    {
        GameObject proj = Instantiate(projectile, projectileSpawn.transform); //creates the projectile at the spawn location
        proj.GetComponent<Projectile>().SetDamage(damageToEnemy); // sets projectile damage
        proj.GetComponent<Projectile>().SetTarget(target); //sets the target for the projectile

    }

    public GameObject GetFirstEnemy()
    {
        GameObject firstEnemy;

        //if some enemies get unexpectedly destroyed while in range, remove them from list
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null)
                {
                    targets.Remove(targets[i]);
                    i--;
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

    public override int GetTowerType()
    {
        return (int)TowerType.CannonTower;
    }
}
