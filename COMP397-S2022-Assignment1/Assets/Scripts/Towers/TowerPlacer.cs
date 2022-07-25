/*  Filename:           TowerPlacer.cs
 *  Author:             Han Bi (301176547)
 *                      Marcus Ngooi (301147411)
 *                      Ikamjot Hundal (301134374)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        July 24, 2022
 *  Description:        For placing towers.
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.
 *                      June 26, 2022 (Marcus Ngooi): Adding resource tower to TowerPlacer.
 *                      June 26, 2022 (Ikamjot Hundal): Adding CannonBall tower to TowerPlacer.
 *                      June 26, 2022 (Yuk Yee Wong): Adding start method to place the tower using TowerPlaceStaticData, calling initialize function in PlaceTower function, and placing the tower to towerContainer.
 *                      July 20, 2022 (Han Bi): Moved alot of the tower specific code to TowerFactory.
 *                      July 22, 2022 (Sukhmannat Singh): Added method to spawn towers on load.
 *                      July 24, 2022 (Marcus Ngooi): Integrated Factory Design pattern to work with load system.
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
        if(!isPreview)
        {
            currentType = towerType;
            goldCost = goldNeeded;
            stoneCost = stoneNeeded;
            woodCost = woodNeeded;
            isPreview = true;

            //Call the Tower Factory to create a Preview tower object
            towerPreview = TowerFactory.instance.CreateTowerPreview(towerType);
        }
    }

    public IEnumerator PlaceTower(Tower.TowerType towerType)
    {
        Destroy(towerPreview);

        //resets the isPreview to false
        isPreview = false;
        //plays the tower placement sound
        SoundManager.instance.PlaySFX(placeSound);
        //calls the Create tower function on TowerFactory
        GameObject towerObject = TowerFactory.instance.CreateTower(towerType, worldPos, Quaternion.identity);
        //set the parent of these towers to a gameObject in the editor heirarchy
        towerObject.transform.parent = towerContainer;
        Tower tower = towerObject.GetComponent<Tower>();
        tower.StartBuilding();
        yield return new WaitForSeconds(tower.GetBuildTime());

        if (tower.getIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
        {
            tower.CompleteBuilding();
        }
    }

    //public IEnumerator PlaceTower(Tower.TowerType towerType)
    //{
    //    Destroy(towerPreview);

    //    if (towerType == Tower.TowerType.CrossbowTower)
    //    {
    //        isPreview = false;
    //        SoundManager.instance.PlaySFX(placeSound);
    //        GameObject towerObject = Instantiate(crossbowTower, worldPos, Quaternion.identity, towerContainer);
    //        Tower tower = towerObject.GetComponent<Tower>();
    //        tower.Intialize(crossbowTowerStaticData);
    //        tower.StartBuilding();
    //        yield return new WaitForSeconds(tower.GetBuildTime());

    //        if (tower.getIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
    //        {
    //            tower.CompleteBuilding();
    //        }
    //    }
    //    if (towerType == Tower.TowerType.ResourceTower)
    //    {
    //        isPreview = false;
    //        SoundManager.instance.PlaySFX(placeSound);
    //        GameObject towerObject = Instantiate(resourceTower, worldPos, Quaternion.identity, towerContainer);
    //        Tower tower = towerObject.GetComponent<Tower>();
    //        tower.Intialize(resourceTowerStaticData);
    //        tower.StartBuilding();
    //        yield return new WaitForSeconds(tower.GetBuildTime());

    //        if (tower.getIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
    //        {
    //            tower.CompleteBuilding();
    //        }
    //    }

    //    if (towerType == Tower.TowerType.CannonTower)
    //    {
    //        isPreview = false;
    //        SoundManager.instance.PlaySFX(placeSound);
    //        GameObject towerObject = Instantiate(cannonTower, worldPos, Quaternion.identity, towerContainer);
    //        Tower tower = towerObject.GetComponent<Tower>();
    //        tower.Intialize(cannonTowerStaticData);
    //        tower.StartBuilding();
    //        yield return new WaitForSeconds(tower.GetBuildTime());

    //        if (tower.getIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
    //        {
    //            tower.CompleteBuilding();
    //        }
    //    }
    //}

    public IEnumerator PlaceTowerOnLoad(Tower.TowerType towerType, Vector3 pos, Quaternion rot, bool isBuilding, float health)
    {
        if (isBuilding == false)
        {
            SoundManager.instance.PlaySFX(placeSound);
            GameObject towerObject = TowerFactory.instance.CreateTower(towerType, pos, rot);
            towerObject.transform.parent = towerContainer;
            Tower tower = towerObject.GetComponent<Tower>();
            tower.StartBuilding();
            yield return null;
            tower.CompleteBuilding();
            tower.health.currentHealth = health;
        }
        else if (isBuilding == true)
        {
            SoundManager.instance.PlaySFX(placeSound);
            GameObject towerObject = TowerFactory.instance.CreateTower(towerType, pos, rot);
            towerObject.transform.parent = towerContainer;
            Tower tower = towerObject.GetComponent<Tower>();
            tower.StartBuilding();
            yield return new WaitForSeconds(tower.GetBuildTime());
            if (tower.getIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
            {
                tower.CompleteBuilding();
            }
        }
    }

    //public IEnumerator PlaceTowerOnLoad(Tower.TowerType towerType, Vector3 pos, Quaternion rot, bool isBuilding, float health)
    //{
    //    if (towerType == Tower.TowerType.CrossbowTower && isBuilding == false)
    //    {
    //        SoundManager.instance.PlaySFX(placeSound);
    //        GameObject towerObject = Instantiate(crossbowTower, pos, rot, towerContainer);
    //        Tower tower = towerObject.GetComponent<Tower>();
    //        tower.Intialize(crossbowTowerStaticData);
    //        tower.StartBuilding();
    //        yield return null;
    //        tower.CompleteBuilding();
    //        tower.health.currentHealth = health;
    //    }
    //    else if (towerType == Tower.TowerType.CrossbowTower && isBuilding == true)
    //    {
    //        SoundManager.instance.PlaySFX(placeSound);
    //        GameObject towerObject = Instantiate(crossbowTower, pos, rot, towerContainer);
    //        Tower tower = towerObject.GetComponent<Tower>();
    //        tower.Intialize(crossbowTowerStaticData);
    //        tower.StartBuilding();
    //        yield return new WaitForSeconds(tower.GetBuildTime());
    //        if (tower.getIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
    //        {
    //            tower.CompleteBuilding();
    //        }
    //    }

    //    if (towerType == Tower.TowerType.ResourceTower && isBuilding == false)
    //    {
    //        SoundManager.instance.PlaySFX(placeSound);
    //        GameObject towerObject = Instantiate(resourceTower, pos, rot, towerContainer);
    //        Tower tower = towerObject.GetComponent<Tower>();
    //        tower.Intialize(resourceTowerStaticData);
    //        tower.StartBuilding();
    //        yield return null;
    //        tower.CompleteBuilding();
    //        tower.health.currentHealth = health;
    //    }
    //    else if (towerType == Tower.TowerType.ResourceTower && isBuilding == true)
    //    {
    //        SoundManager.instance.PlaySFX(placeSound);
    //        GameObject towerObject = Instantiate(resourceTower, pos, rot, towerContainer);
    //        Tower tower = towerObject.GetComponent<Tower>();
    //        tower.Intialize(resourceTowerStaticData);
    //        tower.StartBuilding();
    //        yield return new WaitForSeconds(tower.GetBuildTime());

    //        if (tower.getIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
    //        {
    //            tower.CompleteBuilding();
    //        }
    //    }

    //    if (towerType == Tower.TowerType.CannonTower && isBuilding == false)
    //    {
    //        SoundManager.instance.PlaySFX(placeSound);
    //        GameObject towerObject = Instantiate(cannonTower, pos, rot, towerContainer);
    //        Tower tower = towerObject.GetComponent<Tower>();
    //        tower.Intialize(cannonTowerStaticData);
    //        tower.StartBuilding();
    //        yield return null;
    //        tower.CompleteBuilding();
    //        tower.health.currentHealth = health;
    //    }
    //    else if (towerType == Tower.TowerType.CannonTower && isBuilding == true)
    //    {
    //        SoundManager.instance.PlaySFX(placeSound);
    //        GameObject towerObject = Instantiate(cannonTower, pos, rot, towerContainer);
    //        Tower tower = towerObject.GetComponent<Tower>();
    //        tower.Intialize(cannonTowerStaticData);
    //        tower.StartBuilding();
    //        yield return new WaitForSeconds(tower.GetBuildTime());

    //        if (tower.getIsBuilding()) //if tower is set to is building (ie. the player hasn't spent money to buy the tower)
    //        {
    //            tower.CompleteBuilding();
    //        }
    //    }
    //}

    public void CancelBuy()
    {
        Destroy(towerPreview.gameObject);
        isPreview = false; 
    }


}

