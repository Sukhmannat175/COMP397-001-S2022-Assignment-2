/*  Filename:           TowerPlacer.cs
 *  Author:             Han Bi (301176547)
 *                      Marcus Ngooi (301147411)
 *                      Ikamjot Hundal (301134374)
 *                      Yuk Yee Wong (301234795)
 *                      Sukhmannat Singh (301168420)
 *  Last Update:        August 5, 2022
 *  Description:        For placing towers.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.
 *                      June 26, 2022 (Marcus Ngooi): Adding resource tower to TowerPlacer.
 *                      June 26, 2022 (Ikamjot Hundal): Adding CannonBall tower to TowerPlacer.
 *                      June 26, 2022 (Yuk Yee Wong): Adding start method to place the tower using TowerPlaceStaticData, calling initialize function in PlaceTower function, and placing the tower to towerContainer.
 *                      July 20, 2022 (Han Bi): Moved alot of the tower specific code to TowerFactory.
 *                      July 22, 2022 (Sukhmannat Singh): Added method to spawn towers on load.
 *                      July 24, 2022 (Marcus Ngooi): Integrated Factory Design pattern to work with load system.
 *                      August 1, 2022 (Yuk Yee Wong): Reorganised the code and adapted object pooling.
 *                      August 5, 2022 (Marcus Ngooi): Moved towersPlaced and maxTowersToBePlaced variables from TowerPlacer to GameController.
 *                      August 8, 2022 (Han Bi): Added events and invoke for achievement system.
 *                      August 15, 2022 (Yuk Yee Wong): Fixed world position on y-axis
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    //creates an event here when the first tower is built
    public static event Action FirstTowerBuilt = delegate { };
    public static event Action LastTowerBuilt = delegate { };

    [SerializeField] private bool isPreview = false;
    [SerializeField] private AudioClip placeSound;
    //public int towersPlaced;
    //[SerializeField] int maxTowersToBePlaced = 10;

    [Header("Loaded from Resources")]
    [SerializeField] private TowerStaticData crossbowTowerStaticData;
    [SerializeField] private TowerStaticData cannonTowerStaticData;
    [SerializeField] private TowerStaticData resourceTowerStaticData;

    public Vector3 screenPos;
    public Vector3 worldPos;
    public LayerMask ground = 1<<7;

    Tower.TowerType currentType;
    TowerPreview towerPreview;

    int goldCost;
    int stoneCost;
    int woodCost;



    private void Start()
    {
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
                worldPos.y += 0.5f;
            }

            towerPreview.transform.position = worldPos;

            if (towerPreview.GetIsValidPosition() && InventoryManager.instance.EnoughResources(goldCost, stoneCost, woodCost) 
                && GameController.instance.TowersPlaced < GameController.instance.MaxTowersToBePlaced)
            {
                towerPreview.ChangeRangeColor(new Color(1, 1, 1, 0.4f));

                if (Input.GetMouseButtonDown(0))
                {
                    InventoryManager.instance.BuyTower(goldCost, stoneCost, woodCost);
                    GameController.instance.TowersPlaced++;

                    if(GameController.instance.TowersPlaced == 1)
                    {
                        FirstTowerBuilt.Invoke();

                    }else if(GameController.instance.TowersPlaced == GameController.instance.MaxTowersToBePlaced)
                    {
                        //
                        LastTowerBuilt.Invoke();
                    }

                    StartCoroutine(PlaceTower(currentType));
                }
            }
            else
            {
                towerPreview.ChangeRangeColor(new Color(1, 0, 0, 0.4f));
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
        if(!isPreview)
        {
            currentType = towerType;
            goldCost = goldNeeded;
            stoneCost = stoneNeeded;
            woodCost = woodNeeded;
            isPreview = true;

            //Call the Tower Factory to create a Preview tower object
            towerPreview = TowerFactory.Instance.CreateTowerPreview(towerType);
        }
    }

    public IEnumerator PlaceTower(Tower.TowerType towerType)
    {
        Destroy(towerPreview.gameObject);

        //resets the isPreview to false
        isPreview = false;
        //plays the tower placement sound
        SoundManager.instance.PlaySFX(placeSound);
        //calls the Create tower function on TowerFactory
        Tower tower = TowerFactory.Instance.CreateTower(towerType, worldPos, Quaternion.identity);
        tower.StartBuilding(0);
        yield return new WaitForSeconds(tower.GetBuildTime());

        if (tower.GetIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
        {
            tower.CompleteBuilding();
        }

        //for achievement system
    }

    public IEnumerator PlaceTowerOnLoad(TowerData towerData)
    {
        SoundManager.instance.PlaySFX(placeSound);
        Tower tower = TowerFactory.Instance.CreateTower(towerData.towerType, towerData.towerPosition, towerData.towerRotation);
        tower.StartBuilding(towerData.buildingTime);

        if (towerData.isBuilding == false)
        {
            yield return null;
            tower.CompleteBuilding();
            tower.health.currentHealth = towerData.health;
        }
        else
        {
            yield return new WaitForSeconds(tower.GetBuildTime());
            if (tower.GetIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
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

