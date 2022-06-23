/*Tower.cs
 *Created by: Han Bi 301176547
 *base abstract class for all towers
 *Last update: June 8, 2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tower : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Sets to true if tower is building")]
    protected bool isBuilding;

    [SerializeField]
    [Tooltip("How long in seconds this takes to build")]
    [Range(0, 60)]
    int BUILD_TIME;

    [SerializeField]
    GameObject completeBuildButton;
    [SerializeField]
    float btnPositionOffset;

    [SerializeField]
    int instantCompleteGoldCost = 50;
    [SerializeField]
    int instantCompleteStoneCost = 0;
    [SerializeField]
    int instantCompleteWoodCost = 0;

    public enum TowerType
    {
        CrossbowTower,
        BombTower,
        ResourceTower
    }

    [SerializeField]
    [Tooltip("The time tower will wait before firing again")]
    protected float actionDelay;

    protected abstract void TowerBehaviour();

    private void Start()
    {
        isBuilding = true;
        
    }
    private void Update()
    {
        if (!isBuilding)
        {
            TowerBehaviour();
        }

        //for testing:
        if (Input.GetKeyDown(KeyCode.J))
            {
                GetComponent<Health>().ChangeHealth(-10);
            }

    }
    private void LateUpdate()
    {
        completeBuildButton.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * btnPositionOffset);
    }

    public virtual void AddToTargets(GameObject gameObject) { }

    public virtual void RemoveFromTargets(GameObject gameObject) { }


    public abstract int GetTowerType();

    public int GetBuildTime()
    {
        return BUILD_TIME;
    }

    public void setIsBuilding(bool isBuilding)
    {
        this.isBuilding = isBuilding;
        
    }

    public bool getIsBuilding() 
    {
        return isBuilding;
    }

    public void CompleteBuilding()
    {
        if (InventoryManager.instance.EnoughResources(instantCompleteGoldCost, instantCompleteStoneCost, instantCompleteWoodCost))
        {
            setIsBuilding(false);
            completeBuildButton.SetActive(false);
            GetComponent<Health>().StopDisplayTime();
            InventoryManager.instance.DecreaseResources(instantCompleteGoldCost, instantCompleteStoneCost, instantCompleteWoodCost);
        }

    }

    public void StartBuilding()
    {
        setIsBuilding(true);
        completeBuildButton.SetActive(true);
        GetComponent<Health>().DisplayBuildTime(GetBuildTime());
    }


}



