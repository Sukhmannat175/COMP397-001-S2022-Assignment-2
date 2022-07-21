/*  Filename:           TowerFactory.cs
 *  Author:             Han Bi (301176547)
 *  Description:        For creating towers
 *  Revision History:   July 20, 2022 (Han Bi): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerFactory : MonoBehaviour
{
    //The working tower
    [SerializeField]
    private GameObject crossbowTower;
    [SerializeField]
    private GameObject cannonTower;
    [SerializeField]
    private GameObject resourceTower;

    //A dummy tower for display
    [SerializeField]
    private GameObject crossbowTowerPreview;
    [SerializeField]
    private GameObject cannonTowerPreview;
    [SerializeField]
    private GameObject resourceTowerPreview;


    [Header("Loaded from Resources")]
    [SerializeField] private TowerStaticData crossbowTowerStaticData;
    [SerializeField] private TowerStaticData cannonTowerStaticData;
    [SerializeField] private TowerStaticData resourceTowerStaticData;

    public static TowerFactory instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

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


    public GameObject CreateTower(Tower.TowerType towerType, Vector3 position, Quaternion rotation)
    {
        GameObject tower = null;

        switch (towerType)
        {
            case Tower.TowerType.CrossbowTower:
                tower = CreateCrossbowTower(position, rotation);
                break;

            case Tower.TowerType.CannonTower:
                tower = CreateCannonTower(position, rotation);
                break;

            case Tower.TowerType.ResourceTower:
                tower = CreateResourceTower(position, rotation);
                break;

            default:
                tower = null;
                break;

        }


        return tower;
    }

    public GameObject CreateTowerPreview(Tower.TowerType towerType)
    {
        GameObject tower;

        switch (towerType)
        {
            case Tower.TowerType.CrossbowTower:
                tower = CreateCrossbowTowerPreview();
                break;

            case Tower.TowerType.CannonTower:
                tower = CreateCannonTowerPreview();
                break;

            case Tower.TowerType.ResourceTower:
                tower = CreateResourceTowerPreview();
                break;

            default:
                tower = null;
                break;
        }

        return tower;
    }



    //Helper functions. 

    private GameObject CreateCrossbowTower(Vector3 position, Quaternion rotation)
    {
        GameObject tower = Instantiate(crossbowTower, position, rotation);
        tower.GetComponent<Tower>().Intialize(crossbowTowerStaticData);

        return tower;
    }

    private GameObject CreateCannonTower(Vector3 position, Quaternion rotation)
    {
        GameObject tower = Instantiate(crossbowTower, position, rotation);
        tower.GetComponent<Tower>().Intialize(cannonTowerStaticData);

        return tower;
    }
    private GameObject CreateResourceTower(Vector3 position, Quaternion rotation)
    {
        GameObject tower = Instantiate(crossbowTower, position, rotation);
        tower.GetComponent<Tower>().Intialize(resourceTowerStaticData);
        return tower;
    }

    private GameObject CreateCrossbowTowerPreview()
    {
        GameObject tower = Instantiate(crossbowTowerPreview);
        return tower;
    }

    private GameObject CreateCannonTowerPreview()
    {
        GameObject tower = Instantiate(cannonTowerPreview);
        return tower;
    }

    private GameObject CreateResourceTowerPreview()
    {
        GameObject tower = Instantiate(resourceTowerPreview);
        return tower;
    }





}
