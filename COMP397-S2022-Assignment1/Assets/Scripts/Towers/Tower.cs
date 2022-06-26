/*  Filename:           Tower.cs
 *  Author:             Han Bi (301176547)
 *  Last Update:        June 26, 2022
 *  Description:        Base abstract class for all towers.
 *  Revision History:   June 8, 2022 (Han Bi): Initial script.
 *                      June 26, 2022 (Han Bi): Added tower building time functionality
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] protected int maxHealthValue;
    [SerializeField] protected HealthDisplay healthDisplay;

    [SerializeField]
    [Tooltip("The time tower will wait before firing again")]
    protected float actionDelay;

    Health health;

    public enum TowerType
    {
        CrossbowTower,
        BombTower,
        ResourceTower
    }

    private void Start()
    {
        //healthDisplay.Init(maxHealthValue);
        isBuilding = true;
        TowerStartBehaviour();
        health = GetComponent<Health>();        
    }

    protected abstract void TowerStartBehaviour();


    private void Update()
    {
        if (!isBuilding)
        {
            TowerUpdateBehaviour();
        }
        //Debug only
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(-1);
        }

    }

    private void LateUpdate()
    {
        completeBuildButton.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * btnPositionOffset);
    }

    protected abstract void TowerUpdateBehaviour();

    public void TakeDamage(int damage)
    {
        //healthDisplay.TakeDamage(damage);
        //if (healthDisplay.CurrentHealthValue == 0)
        //{
        //    SoundManager.instance.PlayTowerDestroySfx();
        //    Destroy(gameObject);
        //}

        if (!isBuilding)
        {
            health.ChangeHealth(-damage);

            if (health.currentHealth <= 0)
            {
                SoundManager.instance.PlayTowerDestroySfx();
                Destroy(gameObject);
            }
        }
        
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



