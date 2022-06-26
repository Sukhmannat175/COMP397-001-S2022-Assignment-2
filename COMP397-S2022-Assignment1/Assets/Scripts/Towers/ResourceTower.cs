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
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private int goldIncrease = 0;
    [SerializeField] private int stoneIncrease = 50;
    [SerializeField] private int woodIncrease = 25;
    public TowerType type;

    [HideInInspector] public TowerData towerData;

    private IEnumerator Collect()
    {
        coolingDown = true;
        InventoryManager.instance.CollectResources(goldIncrease, stoneIncrease, woodIncrease);
        yield return new WaitForSeconds(actionDelay);
        coolingDown = false;
    }
    public override int GetTowerType()
    {
        return (int)TowerType.ResourceTower;
    }

    protected override void TowerStartBehaviour()
    {
        if (string.IsNullOrEmpty(towerData.towerId))
        {
            towerData.towerId = this.GetTowerType().ToString() + Random.Range(0, int.MaxValue).ToString();
            towerData.towerType = TowerType.ResourceTower;
            GameController.instance.current.towers.Add(towerData);
        }
    }

    protected override void TowerUpdateBehaviour()
    {
        if (coolingDown == false)
        {
            StartCoroutine(Collect());
        }

        towerData.towerPosition = transform.position;
        towerData.towerRotation = transform.rotation;
    }
}
