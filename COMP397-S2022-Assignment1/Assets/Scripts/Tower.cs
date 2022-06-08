/*
 *Created by: Han Bi 301176547
 *Script used for tower behaviour
 *Last update: June 8, 2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Tower Range")]
    private GameObject attackZone;

    [SerializeField]
    [Tooltip("Prefab of projectile this tower will shoot")]
    private GameObject projectile;

    [SerializeField]
    [Tooltip("The location the Projectile will shoot from")]
    private GameObject projectileSpawn;

    [SerializeField]
    [Tooltip("The time tower will wait before firing again")]
    private float shotDelay;


    [Header("Tower Cost:")]
    [SerializeField]
    [Tooltip("Gold Cost")]
    int gold = 0;

    [SerializeField]
    [Tooltip("Wood Cost")]
    int wood = 0;

    [SerializeField]
    [Tooltip("Stone Cost")]
    int stone = 0;
    



    //for testing
    public List<GameObject> targets = new List<GameObject> { }; //list of all enemies in range
    [SerializeField] private bool isWaiting = false; //used to flag tower cooldown in Coroutine
    [SerializeField] private GameObject currentTarget = null;


    // Update is called once per frame
    void Update()
    {
        UpdateCurrentTarget();
        
        if(isWaiting == false && currentTarget != null)
        {
            StartCoroutine(Shoot());
        }

    }

    private IEnumerator Shoot()
    {

        isWaiting = true; //stops the coroutine from being called again
        ShootProjectile(currentTarget);

        yield return new WaitForSeconds(shotDelay);

        isWaiting = false; // releases the coroutine to be called

    }

    private void ShootProjectile(GameObject target)
    {
        GameObject proj = Instantiate(projectile, projectileSpawn.transform); //creates the projectile at the spawn location

        proj.GetComponent<Projectile>().SetTarget(target); //sets the target for the projectile
        
    }

    public GameObject GetFirstEnemy()
    {
        GameObject firstEnemy;

        //if some enemies get unexpectedly destroyed while in range, remove them from list
        if(targets.Count > 0)
        {
            for(int i = 0; i < targets.Count; i++)
            {
                if(targets[i] == null)
                {
                    targets.Remove(targets[i]);
                    i--;
                }
            }
        }

        if(targets.Count == 0)
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

    public void AddToTargets(GameObject target) //adds a gameobject to target list
    {
        targets.Add(target);
        UpdateCurrentTarget();
    }

    public void RemoveFromTargets(GameObject target) //removes a gameobject from target list
    {

        targets.Remove(target);
        UpdateCurrentTarget();
        
    }

    public void UpdateCurrentTarget() //sets currentTarget to first Target
    {
        currentTarget = GetFirstEnemy();
    }
}
