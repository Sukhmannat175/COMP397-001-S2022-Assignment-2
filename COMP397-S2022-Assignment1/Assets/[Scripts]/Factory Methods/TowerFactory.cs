/*  Filename:           TowerFactory.cs
 *  Author:             Han Bi (301176547)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        August 1, 2022
 *  Description:        For creating towers
 *  Revision History:   July 20, 2022 (Han Bi): Initial script.
 *                      August 1, 2022 (Yuk Yee Wong): Reorganised the code and adapted object pooling.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    // The working tower
    [SerializeField] private TowerPoolManager crossbowTowerPoolManager;
    [SerializeField] private TowerPoolManager cannonTowerPoolManager;
    [SerializeField] private TowerPoolManager resourceTowerPoolManager;

    // A dummy tower for display
    [SerializeField] private TowerPreview crossbowTowerPreview;
    [SerializeField] private TowerPreview cannonTowerPreview;
    [SerializeField] private TowerPreview resourceTowerPreview;

    [Header("Loaded from Resources")]
    [SerializeField] private TowerStaticData crossbowTowerStaticData;
    [SerializeField] private TowerStaticData cannonTowerStaticData;
    [SerializeField] private TowerStaticData resourceTowerStaticData;

    public static TowerFactory Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
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

    public Tower CreateTower(Tower.TowerType towerType, Vector3 position, Quaternion rotation)
    {
        Tower tower = null;

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

    public TowerPreview CreateTowerPreview(Tower.TowerType towerType)
    {
        TowerPreview towerPreview;

        switch (towerType)
        {
            case Tower.TowerType.CrossbowTower:
                towerPreview = CreateCrossbowTowerPreview();
                break;

            case Tower.TowerType.CannonTower:
                towerPreview = CreateCannonTowerPreview();
                break;

            case Tower.TowerType.ResourceTower:
                towerPreview = CreateResourceTowerPreview();
                break;

            default:
                towerPreview = null;
                break;
        }

        return towerPreview;
    }

    // Helper functions
    private Tower CreateCrossbowTower(Vector3 position, Quaternion rotation)
    {
        return crossbowTowerPoolManager.GetPooledTower(position, rotation, crossbowTowerStaticData);
    }

    private Tower CreateCannonTower(Vector3 position, Quaternion rotation)
    {
        return cannonTowerPoolManager.GetPooledTower(position, rotation, cannonTowerStaticData);
    }
    private Tower CreateResourceTower(Vector3 position, Quaternion rotation)
    {
        return resourceTowerPoolManager.GetPooledTower(position, rotation, resourceTowerStaticData);
    }

    public void ReturnPooledCrossbowTower(Tower crossbowTower)
    {
        crossbowTowerPoolManager.ReturnPooledTower(crossbowTower);
    }

    public void ReturnPooledCannonTower(Tower cannonTower)
    {
        cannonTowerPoolManager.ReturnPooledTower(cannonTower);
    }

    public void ReturnPooledResourceTower(Tower resourceTower)
    {
        resourceTowerPoolManager.ReturnPooledTower(resourceTower);
    }

    public void ReturnAllTowers()
    {
        crossbowTowerPoolManager.ReturnAllPooledObjects();
        cannonTowerPoolManager.ReturnAllPooledObjects();
        resourceTowerPoolManager.ReturnAllPooledObjects();
    }

    private TowerPreview CreateCrossbowTowerPreview()
    {
        return Instantiate(crossbowTowerPreview);
    }

    private TowerPreview CreateCannonTowerPreview()
    {
        return Instantiate(cannonTowerPreview);
    }

    private TowerPreview CreateResourceTowerPreview()
    {
        return Instantiate(resourceTowerPreview);
    }
}
