/*
 *Created by: Han Bi 301176547
 *Script used for tower behaviour
 *Last update: June 7, 2022
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
    
    //for testing
    [SerializeField] private List<GameObject> targets = new List<GameObject> {}; //list of all enemies in range
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

        proj.GetComponent<Projectile>().SetTarget(target); //sets the target for the project to the passed through value
        
    }

    public GameObject GetFirstTarget()
    {

        while (targets.Count > 0 && targets[0] == null) //if some enemies get unexpectedly destroyed while in range, remove them from list
        {
            targets.RemoveAt(0);
        }

        try
        {
            return targets[0];
        }
        catch
        {
            return null;
        }
    }

    public void AddToTargets(GameObject target) //adds a gameobject to target list
    {
        targets.Add(target);
    }

    public void RemoveFromTargets(GameObject target) //removes a gameobject from target list
    {

        targets.Remove(target);
        
    }

    public void UpdateCurrentTarget() //sets currentTarget to first Target
    {
        currentTarget = GetFirstTarget();
    }
}
