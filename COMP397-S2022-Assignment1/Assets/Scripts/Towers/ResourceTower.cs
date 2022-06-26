/*  Filename:           ResourceTower.cs
 *  Author:             Han Bi (301176547)
 *                      Marcus Ngooi (301147411)
 *  Last Update:        June 25, 2022
 *  Description:        Use for resource tower.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.
 *                      June 25, 2022 (Marcus Ngooi): Added logic for collector tower.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTower : Tower
{
    [SerializeField] private bool coolingDown = false;
    
    private IEnumerator Collect()
    {
        coolingDown = true;
        InventoryManager.instance.CollectResources(0, 10, 5);
        yield return new WaitForSeconds(actionDelay);
        coolingDown = false;
    }
    public override int GetTowerType()
    {
        return (int)TowerType.ResourceTower;
    }

    protected override void TowerStartBehaviour()
    {
        
    }

    protected override void TowerUpdateBehaviour()
    {
        if (coolingDown == false)
        {
            StartCoroutine(Collect());
        }
    }
}
