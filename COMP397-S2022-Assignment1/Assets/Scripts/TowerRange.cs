/*  Filename:           TowerRange.cs
 *  Author:             Han Bi (301176547)
 *  Last Update:        June 7, 2022
 *  Description:        Script used for tower behaviour. This is a sphere collisionBox that detect all enemies within range.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    [SerializeField][Tooltip("Please make sure this object is parented to a tower script")]
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

    private void OnTriggerStay(Collider other)
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
