/*  Filename:           TowerPlacer.cs
 *  Author:             Han Bi (301176547)
 *                      Marcus Ngooi (301147411)
 *                      Ikamjot Hundal (301134374)
 *  Last Update:        June 26, 2022
 *  Description:        For placing towers.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.
 *                      June 26, 2022 (Marcus Ngooi): Adding resource tower to TowerPlacer.
 *                      June 26, 2022 (Ikamjot Hundal): Adding CannonBall tower to TowerPlacer.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] GameObject crossbowTower;
    [SerializeField] GameObject crossbowTowerPreview;
    [SerializeField] private Transform towerContainer;

    [SerializeField] GameObject resourceTower;
    [SerializeField] GameObject resourceTowerPreview;

    [SerializeField] GameObject cannonTower;
    [SerializeField] GameObject cannonTowerPreview;

    GameObject towerPreview;
    [SerializeField] bool isPreview = false;

    [SerializeField] AudioClip placeSound;

    [Header("Loaded from Resources")]
    [SerializeField] private TowerStaticData crossbowTowerStaticData;
    [SerializeField] private TowerStaticData cannonTowerStaticData;
    [SerializeField] private TowerStaticData resourceTowerStaticData;

    public Vector3 screenPos;
    public Vector3 worldPos;

    public LayerMask ground = 1<<7;

    Tower.TowerType currentType;

    int goldCost;
    int stoneCost;
    int woodCost;

    [SerializeField] int towersPlaced;

    private void Start()
    {
        towersPlaced = 0;
        // Load data from scriptable object
        TowerPlacerStaticData towerPlacerStaticData = Resources.Load<TowerPlacerStaticData>("ScriptableObjects/TowerPlacerStaticData");
        if (towerPlacerStaticData != null)
        {
            crossbowTowerStaticData = towerPlacerStaticData.towerStaticDataList.Find(x => x.tower == Tower.TowerType.CrossbowTower);
            cannonTowerStaticData = towerPlacerStaticData.towerStaticDataList.Find(x => x.tower == Tower.TowerType.CannonTower);
            resourceTowerStaticData = towerPlacerStaticData.towerStaticDataList.Find(x => x.tower == Tower.TowerType.ResourceTower);
        }
        else
        {
            Debug.LogError("towerPlacerStaticData cannot be loaded");
        }
    }

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

            if (towerPreview.GetComponent<TowerPreview>().GetIsValidPosition() && InventoryManager.instance.EnoughResources(goldCost, stoneCost, woodCost) && towersPlaced < 10)
            {
                towerPreview.GetComponent<TowerPreview>().ChangeRangeColor(new Color(1, 1, 1, 0.4f));

                if (Input.GetMouseButtonDown(0) && towersPlaced < 10)
                {
                    InventoryManager.instance.BuyTower(goldCost, stoneCost, woodCost);
                    towersPlaced++;
                    StartCoroutine(PlaceTower(currentType));

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
                towerPreview = Instantiate(crossbowTowerPreview, towerContainer);
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
                towerPreview = Instantiate(resourceTowerPreview, towerContainer);
            }
        }
        if (towerType == Tower.TowerType.CannonTower)
        {
            if (!isPreview)
            {
                currentType = towerType;
                goldCost = goldNeeded;
                stoneCost = stoneNeeded;
                woodCost = woodNeeded;

                isPreview = true;
                towerPreview = Instantiate(cannonTowerPreview, towerContainer);
            }
        }
    }

    public IEnumerator PlaceTower(Tower.TowerType towerType)
    {

        Destroy(towerPreview);

        if (towerType == Tower.TowerType.CrossbowTower)
        {
            isPreview = false;
            SoundManager.instance.PlaySFX(placeSound);
            GameObject towerObject = Instantiate(crossbowTower, worldPos, Quaternion.identity, towerContainer);
            Tower tower = towerObject.GetComponent<Tower>();
            tower.Intialize(crossbowTowerStaticData);
            tower.StartBuilding();
            yield return new WaitForSeconds(tower.GetBuildTime());

            if (tower.getIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
            {
                tower.CompleteBuilding();
            }
        }
        if (towerType == Tower.TowerType.ResourceTower)
        {
            isPreview = false;
            SoundManager.instance.PlaySFX(placeSound);
            GameObject towerObject = Instantiate(resourceTower, worldPos, Quaternion.identity, towerContainer);
            Tower tower = towerObject.GetComponent<Tower>();
            tower.Intialize(resourceTowerStaticData);
            tower.StartBuilding();
            yield return new WaitForSeconds(tower.GetBuildTime());

            if (tower.getIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
            {
                tower.CompleteBuilding();
            }
        }

        if (towerType == Tower.TowerType.CannonTower)
        {
            isPreview = false;
            SoundManager.instance.PlaySFX(placeSound);
            GameObject towerObject = Instantiate(cannonTower, worldPos, Quaternion.identity, towerContainer);
            Tower tower = towerObject.GetComponent<Tower>();
            tower.Intialize(cannonTowerStaticData);
            tower.StartBuilding();
            yield return new WaitForSeconds(tower.GetBuildTime());

            if (tower.getIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
            {
                tower.CompleteBuilding();
            }
        }
    }

    public void CancelBuy()
    {
        Destroy(towerPreview.gameObject);
        isPreview = false; 
    }


}

