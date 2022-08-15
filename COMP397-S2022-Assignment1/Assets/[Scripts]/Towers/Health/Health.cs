/*  Filename:           Health.cs
 *  Author:             Han Bi (301176547)
 *                      Yuk Yee Wong (301234795)
 *  Last Update:        August, 2022
 *  Description:        Health Script
 *  Revision History:   June 26, 2022 (Han Bi): Initial script. 
 *                      June 26, 2022 (Yuk Yee Wong): Added a SetMaxHealth function
 *                      August 1, 2022 (Yuk Yee Wong): Updated builiding time from save data.
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    //these two track when an object of type Health has be added to the scene
    public static event Action<Health> OnHealthAdded = delegate { };
    public static event Action<Health> OnHealthRemoved = delegate { };

    [SerializeField]
    private float maxHealth = 100;
    public float currentHealth;

    private float buildingTime = 0;
    private bool showBuildingTime = false;
    private float BUILD_TIME;

    public float BuildingTime { get { return buildingTime; } }

    //this will track when health is updated
    public event Action<float> OnHealthUpdated = delegate { };

    //this will track when building time is updated
    public event Action<float> OnBuildingTimeUpdated = delegate { };

    void OnEnable()
    {
        currentHealth = maxHealth;
        OnHealthAdded(this);
 
    }

    // Update is called once per frame
    void Update()
    {
        if (showBuildingTime)
        {
            UpdateBuildingTime();
        }

    }


    public void ChangeHealth(float amount)
    {
        currentHealth += amount;
        float fillAmount = currentHealth / maxHealth;
        OnHealthUpdated(fillAmount);

    }

    public void DisplayBuildTime(float BUILD_TIME, float buildingTime)
    {
        showBuildingTime = true;
        this.BUILD_TIME = BUILD_TIME;
        this.buildingTime = buildingTime;
    }

    private void UpdateBuildingTime()
    {
        buildingTime += Time.deltaTime / BUILD_TIME;

        if (buildingTime > 1)
        {

            Mathf.Clamp(buildingTime, 0, 1);
            OnHealthUpdated(buildingTime);
            showBuildingTime = false;
        }

        OnHealthUpdated(buildingTime);
    }

    public void StopDisplayTime()
    {
        showBuildingTime = false;
        ChangeHealth(0);
    }

    private void OnDisable()
    {
        OnHealthRemoved(this);
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }


}
