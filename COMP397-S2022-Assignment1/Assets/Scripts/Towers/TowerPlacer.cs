/*  Filename:           TowerPlacer.cs
 *  Author:             Han Bi (301176547)
 *                      Marcus Ngooi (301147411)
 *  Last Update:        June 26, 2022
 *  Description:        For placing towers.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.
 *                      June 26, 2022 (Marcus Ngooi): Adding resource tower to TowerPlacer.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] GameObject crossbowTower;
    [SerializeField] GameObject crossbowTowerPreview;

    [SerializeField] GameObject resourceTower;
    [SerializeField] GameObject resourceTowerPreview;

    GameObject towerPreview;
    [SerializeField] bool isPreview = false;

    [SerializeField] AudioClip placeSound;

    public Vector3 screenPos;
    public Vector3 worldPos;

    public LayerMask ground = 1<<7;

    Tower.TowerType currentType;

    int goldCost;
    int stoneCost;
    int woodCost;

    // Update is called once per frame
    void Update()
    {
        
        if (isPreview)
        {

            screenPos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out RaycastHit hit, 100, ground))
            {
                worldPos = hit.transform.position;
                worldPos.y += 1;
            }

            towerPreview.transform.position = worldPos;

            if (towerPreview.GetComponent<TowerPreview>().GetIsValidPosition() && InventoryManager.instance.EnoughResources(goldCost, stoneCost, woodCost))
            {
                towerPreview.GetComponent<TowerPreview>().ChangeRangeColor(new Color(1, 1, 1, 0.4f));

                if (Input.GetMouseButtonDown(0))
                {
                    InventoryManager.instance.BuyTower(goldCost, stoneCost, woodCost);
                    PlaceTower(currentType);
                    
                }
            }
            else
            {
                towerPreview.GetComponent<TowerPreview>().ChangeRangeColor(new Color(1, 0, 0, 0.4f));
            }
                
            if (Input.GetMouseButtonDown(1))
            {
                CancelBuy();
            }
        }
    }

    public void PreviewTower(Tower.TowerType towerType, int goldNeeded, int stoneNeeded, int woodNeeded)
    {
        //if already showing tower, won't show another one until user cancels
        if(towerType == Tower.TowerType.CrossbowTower)
        {
            if(!isPreview)
            {
                currentType = towerType;
                goldCost = goldNeeded;
                stoneCost = stoneNeeded;
                woodCost = woodNeeded;

                isPreview = true;
                towerPreview = Instantiate(crossbowTowerPreview);
            }
        }
        if (towerType == Tower.TowerType.ResourceTower)
        {
            if (!isPreview)
            {
                currentType = towerType;
                goldCost = goldNeeded;
                stoneCost = stoneNeeded;
                woodCost = woodNeeded;

                isPreview = true;
                towerPreview = Instantiate(resourceTowerPreview);
            }
        }
    }

    public void PlaceTower(Tower.TowerType towerType)
    {
        if(towerType == Tower.TowerType.CrossbowTower)
        {
            isPreview = false;
            SoundManager.instance.PlaySFX(placeSound);
            GameObject tower = Instantiate(crossbowTower, worldPos, Quaternion.identity);
        }
        if (towerType == Tower.TowerType.ResourceTower)
        {
            isPreview = false;
            SoundManager.instance.PlaySFX(placeSound);
            GameObject tower = Instantiate(resourceTower, worldPos, Quaternion.identity);
        }
        Destroy(towerPreview);
    }

    public void CancelBuy()
    {
        Destroy(towerPreview.gameObject);
        isPreview = false; 
    }


}

