/*  Filename:           TowerPlacer.cs
 *  Author:             Han Bi (301176547)
 *                      Marcus Ngooi (301147411)
 *                      Ikamjot Hundal (301134374)
 *  Last Update:        June 26, 2022
 *  Description:        For placing tower objects
 *  Revision History:   June 7, 2022 (Han Bi): Initial script.
 *                      June 26, 2022 (Marcus Ngooi): Adding resource tower to TowerPlacer.
 *                      June 26, 2022 (Ikamjot Hundal): Adding CannonBall tower to TowerPlacer.
 *                      July 20, 2022 (Han Bi) Moved alot of the tower specific code to TowerFactory
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{

    [SerializeField] private Transform towerContainer;

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

    public static TowerPlacer instance;

    [SerializeField] int towersPlaced;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        towersPlaced = 0;

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
        if (!isPreview)
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

    public void CancelBuy()
    {
        Destroy(towerPreview.gameObject);
        isPreview = false; 
    }


}

