/*  Filename:           Tower.cs
 *  Author:             Han Bi (301176547)
 *  Last Update:        June 26, 2022
 *  Description:        Base abstract class for all towers.
 *  Revision History:   June 8, 2022 (Han Bi): Initial script.
 *                      June 26, 2022 (Sukhmannat Singh): Added logic for deleting destroyed objects from save file 
 *                      June 26, 2022 (Han Bi): Added tower building time functionality
 *                      June 26, 2022 (Yuk Yee Wong): Added initialize function by using tower static data
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

    private TowerData removeTower;

    public enum TowerType
    {
        CrossbowTower,
        CannonTower,
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

    public void Intialize(TowerStaticData data)
    {
        health = GetComponent<Health>();

        BUILD_TIME = data.buildTime;
        health.SetMaxHealth(data.hp);
        actionDelay = data.interval;
        damageToEnemy = data.damageToEnemy;
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
            health = GetComponent<Health>();
            health.ChangeHealth(-damage);

            if (health.currentHealth <= 0)
            {
                SoundManager.instance.PlayTowerDestroySfx();

                foreach (TowerData towerData in GameController.instance.current.towers)
                {
                    if (towerData.towerId == this.id)
                    {
                        removeTower = towerData;
                    }
                }
                GameController.instance.current.towers.Remove(removeTower);

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
        setIsBuilding(false);
        completeBuildButton.SetActive(false);
        GetComponent<Health>().StopDisplayTime();
    }

    public void InstantComplete()
    {
        if (InventoryManager.instance.EnoughResources(instantCompleteGoldCost, instantCompleteStoneCost, instantCompleteWoodCost)){

            InventoryManager.instance.DecreaseResources(instantCompleteGoldCost, instantCompleteStoneCost, instantCompleteWoodCost);
            CompleteBuilding();
        }
            
        
    }

    public void StartBuilding()
    {
        setIsBuilding(true);
        completeBuildButton.SetActive(true);
        GetComponent<Health>().DisplayBuildTime(GetBuildTime());
    }
}



