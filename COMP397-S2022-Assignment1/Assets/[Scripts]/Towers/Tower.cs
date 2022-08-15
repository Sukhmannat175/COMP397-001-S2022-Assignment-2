/*  Filename:           Tower.cs
 *  Author:             Han Bi (301176547)
 *                      Sukhmannat Singh (301168420)
 *                      Yuk Yee Wong (301234795)
 *                      Marcus Ngooi (301147411)
 *  Last Update:        August 5, 2022
 *  Description:        Base abstract class for all towers.
 *  Revision History:   June 8, 2022 (Han Bi): Initial script.
 *                      June 26, 2022 (Sukhmannat Singh): Added logic for deleting destroyed objects from save file 
 *                      June 26, 2022 (Han Bi): Added tower building time functionality
 *                      June 26, 2022 (Yuk Yee Wong): Added initialize function by using tower static data
 *                      August 5, 2022 (Marcus Ngooi): Decrement down towersPlaced variable from GameController when tower is destroyed.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Sets to true if tower is building")]
    protected bool isBuilding = true;

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
    protected int damageToEnemy; // damage to enemy by shooting projectile

    [HideInInspector] public string id;
    public Health health;

    [HideInInspector] public TowerData towerData; // Save Data

    private TowerData removeTower;

    [Header("For Testing")]
    //for testing
    [SerializeField] protected bool coolingDown = false; //used to flag tower cooldown in Coroutine

    public enum TowerType
    {
        CrossbowTower,
        CannonTower,
        ResourceTower
    }

    private void Start()
    {       
        TowerStartBehaviour();
    }

    protected abstract TowerType towerType { get; }

    protected abstract string idPrefix { get; }

    protected virtual void TowerStartBehaviour()
    {
    }

    protected void RefreshEnemyData()
    {
        id = idPrefix + Random.Range(0, int.MaxValue).ToString();

        if (string.IsNullOrEmpty(towerData.towerId))
        {
            towerData.towerId = id;
            towerData.towerType = towerType;
            towerData.towerPosition = transform.position;
            towerData.towerRotation = transform.rotation;
            towerData.isBuilding = GetIsBuilding();
            towerData.buildingTime = GetBuildingTime();
            GameController.instance.current.towers.Add(towerData);
        }
    }

    protected abstract void ReturnToPool();


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

    public void Intialize(TowerStaticData data)
    {
        if (health == null)
            health = GetComponent<Health>();

        BUILD_TIME = data.buildTime;
        health.SetMaxHealth(data.hp);
        actionDelay = data.interval;
        damageToEnemy = data.damageToEnemy;

        ResetTower();

        RefreshEnemyData();
    }

    protected virtual void ResetTower()
    {
        coolingDown = false;
        isBuilding = true;
    }

    protected abstract void TowerUpdateBehaviour();

    public void TakeDamage(int damage)
    {
        if (!isBuilding)
        {
            health.ChangeHealth(-damage);

            if (health.currentHealth <= 0)
            {
                SoundManager.instance.PlayTowerDestroySfx();
                GameController.instance.TowersPlaced--;

                foreach (TowerData towerData in GameController.instance.current.towers)
                {
                    if (towerData.towerId == this.id)
                    {
                        removeTower = towerData;
                    }
                }
                GameController.instance.current.towers.Remove(removeTower);

                ReturnToPool();
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


    public void SetIsBuilding(bool isBuilding)
    {
        this.isBuilding = isBuilding;

    }

    public bool GetIsBuilding()
    {
        return isBuilding;
    }

    public float GetBuildingTime()
    {
        return health.BuildingTime;
    }

    public void CompleteBuilding()
    {
        SetIsBuilding(false);
        completeBuildButton.SetActive(false);
        health.StopDisplayTime();
    }

    public void InstantComplete()
    {
        if (InventoryManager.instance.EnoughResources(instantCompleteGoldCost, instantCompleteStoneCost, instantCompleteWoodCost)){

            InventoryManager.instance.DecreaseResources(instantCompleteGoldCost, instantCompleteStoneCost, instantCompleteWoodCost);
            CompleteBuilding();
        }
            
        
    }

    public void StartBuilding(float buildingTime)
    {
        SetIsBuilding(true);
        completeBuildButton.SetActive(true);
        health.DisplayBuildTime(GetBuildTime(), buildingTime);
    }
}



