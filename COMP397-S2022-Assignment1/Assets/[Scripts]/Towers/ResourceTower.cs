/*  Filename:           ResourceTower.cs
 *  Author:             Han Bi (301176547)
 *                      Marcus Ngooi (301147411)
 *                      Sukhmannat Singh (301168420)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        August 1, 2022
 *  Description:        Use for resource tower.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.                      
 *                      June 25, 2022 (Marcus Ngooi): Added logic for collector tower.
 *                      June 26, 2022 (Sukhmannat Singh): Added logic to add data to save file
 *                      August 1, 2022 (Yuk Yee Wong): Reorganised the code and adapted object pooling.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTower : Tower
{
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private int goldIncrease = 0;
    [SerializeField] private int stoneIncrease = 50;
    [SerializeField] private int woodIncrease = 25;
    public TowerType type;

    protected override string idPrefix { get { return "ResourceTower"; } }
    protected override TowerType towerType { get { return TowerType.ResourceTower; } }

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

    protected override void TowerUpdateBehaviour()
    {
        if (coolingDown == false)
        {
            StartCoroutine(Collect());
        }
        towerData.isBuilding = GetIsBuilding();
        towerData.health = health.currentHealth;
    }

    protected override void ReturnToPool()
    {
        TowerFactory.Instance.ReturnPooledResourceTower(this);
    }
}
