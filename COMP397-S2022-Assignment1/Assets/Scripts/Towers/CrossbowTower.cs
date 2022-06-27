/*  Filename:           CrossbowTower.cs
 *  Author:             Han Bi (301176547)
 *  Last Update:        June 7, 2022
 *  Description:        Use for crossbow tower projectiles.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowTower : Tower
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

    [HideInInspector] public TowerData towerData;
    private List<GameObject> targets  = new List<GameObject>();

    protected GameObject currentTarget = null;

    protected override void TowerStartBehaviour()
    {
        targets = new List<GameObject>();

        if (string.IsNullOrEmpty(towerData.towerId))
        {
            towerData.towerId = "ResourseTower" + Random.Range(0, int.MaxValue).ToString();
            towerData.towerType = TowerType.CrossbowTower;
            GameController.instance.current.towers.Add(towerData);
        }
    }

    //for testing
    [SerializeField] private bool coolingDown = false; //used to flag tower cooldown in Coroutine
    
    protected override void TowerUpdateBehaviour()
    {
        UpdateCurrentTarget();

        if (coolingDown == false && currentTarget != null)
        {
            StartCoroutine(Shoot());
        }

        towerData.towerPosition = transform.position;
        towerData.towerPosition = transform.position;
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

    public override void  RemoveFromTargets(GameObject gameObject)
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
        return (int)TowerType.CrossbowTower;
    }
}
