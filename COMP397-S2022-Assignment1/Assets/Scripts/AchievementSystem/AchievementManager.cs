/*  Filename:           AchievementManager.cs
 *  Author:             Han Bi (301176547)
 *  Last Update:        August 8, 2022
 *  Description:        For placing towers.
 *  Revision History:   August 8, 2022 (Han Bi): Initial script.
 *  
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    /// <summary>
    /// List of Achievements
    /// firstTower - build a tower for the first time
    /// tenthTower - build max(10) towers for the first time
    /// firstBlood - kill a monster for the first time
    /// bloodbath - kill 10 monsters
    /// </summary>
    /// 

    public enum Achievement
    {
        FirstTower,
        LastTower,
        FirstBlood,
        Bloodbath
    }

    [SerializeField]
    private bool firstTower = false;
    [SerializeField]
    private bool lastTower = false;
    [SerializeField]
    private bool firstBlood = false;
    [SerializeField]
    private bool bloodbath = false;

    public static AchievementManager instance;

    int enemiesDestroyed = 0;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //subscribe to all events that are required
        TowerPlacer.FirstTowerBuilt += CheckFirstTowerAchievement;
        TowerPlacer.LastTowerBuilt += CheckLastTowerAchievement;
        EnemyBaseBehaviour.EnemyKilled += CheckFirstBloodAchievement;
        EnemyBaseBehaviour.EnemyKilled += CheckBloodbathAchievement;
    }


    

    private void CheckFirstTowerAchievement()
    {
        if (!firstTower)
        {
            firstTower = true;
            //unsubscribe from this so that we don't need to keep listening for it
            TowerPlacer.FirstTowerBuilt -= CheckFirstTowerAchievement;
            AchievementDisplay.instance.ShowPopup(Achievement.FirstTower);
        }

        
    }

    private void CheckLastTowerAchievement()
    {
        if (!lastTower)
        {
            lastTower = true;
            TowerPlacer.LastTowerBuilt -= CheckLastTowerAchievement;
            AchievementDisplay.instance.ShowPopup(Achievement.LastTower);
        }

    }

    private void CheckFirstBloodAchievement()
    {
        if (!firstBlood)
        {
            firstBlood = true;
            EnemyBaseBehaviour.EnemyKilled -= CheckFirstBloodAchievement;
            AchievementDisplay.instance.ShowPopup(Achievement.FirstBlood);
        }

    }

    private void CheckBloodbathAchievement()
    {
        if (!bloodbath)
        {
            enemiesDestroyed++;
            if(enemiesDestroyed >= 10)
            {
                bloodbath = true;
                EnemyBaseBehaviour.EnemyKilled -= CheckBloodbathAchievement;
                AchievementDisplay.instance.ShowPopup(Achievement.Bloodbath);
            }
        }


    }





}
