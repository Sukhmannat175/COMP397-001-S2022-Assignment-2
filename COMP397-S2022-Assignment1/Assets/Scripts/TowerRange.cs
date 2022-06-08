/*
 *Created by: Han Bi 301176547
 *Script used for tower behaviour
 *This is a sphere collisionBox that detect all enemies within range
 *Last update: June 7, 2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    Tower parentTower;
    
    // Start is called before the first frame update
    void Start()
    {
        parentTower = GetComponentInParent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            parentTower.AddToTargets(other.gameObject);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            parentTower.RemoveFromTargets(other.gameObject);
        }
        
    }
}
