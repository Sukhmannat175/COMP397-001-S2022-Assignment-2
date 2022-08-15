/*  Filename:           AchievementManager.cs
 *  Author:             Han Bi (301176547)
 *  Last Update:        August 13, 2022
 *  Description:        For achievements.
 *  Revision History:   August 8, 2022 (Han Bi): Initial script.
 *                      August 13, 2022 (Marcus Ngooi): Made properties for achievement fields allowing access from GameController script to save and load. Changed enemiesKilled to the same variable tracked in the GameController.
 *                      August 14, 2022 (Marcus Ngooi): Shifting events to the GameController.cs to generalize first blood, first tower, etc. to achievements, quests, tutorial.
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

    //public enum Achievement
    //{
    //    FirstTower,
    //    LastTower,
    //    FirstBlood,
    //    Bloodbath
    //}

    //[SerializeField]
    //private bool firstTower = false;
    //[SerializeField]
    //private bool lastTower = false;
    //[SerializeField]
    //private bool firstBlood = false;
    //[SerializeField]
    //private bool bloodbath = false;

    public static AchievementManager instance;

    //// Properties
    //public bool FirstTower { get { return firstTower; } set { firstTower = value; } }
    //public bool LastTower { get { return lastTower; } set { lastTower = value; } }
    //public bool FirstBlood { get { return firstBlood; } set { firstBlood = value; } }
    //public bool Bloodbath { get { return bloodbath; } set { bloodbath = value; } }

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
        if (!GameController.instance.FirstTower)
        {
            GameController.instance.FirstTower = true;
            //unsubscribe from this so that we don't need to keep listening for it
            TowerPlacer.FirstTowerBuilt -= CheckFirstTowerAchievement;
            AchievementDisplay.instance.ShowPopup(GameController.Event.FirstTower);
        }
    }

    private void CheckLastTowerAchievement()
    {
        if (!GameController.instance.LastTower)
        {
            GameController.instance.LastTower = true;
            TowerPlacer.LastTowerBuilt -= CheckLastTowerAchievement;
            AchievementDisplay.instance.ShowPopup(GameController.Event.LastTower);
        }
    }

    private void CheckFirstBloodAchievement()
    {
        if (!GameController.instance.FirstBlood)
        {
            GameController.instance.FirstBlood = true;
            EnemyBaseBehaviour.EnemyKilled -= CheckFirstBloodAchievement;
            AchievementDisplay.instance.ShowPopup(GameController.Event.FirstBlood);
        }

    }

    private void CheckBloodbathAchievement()
    {
        if (!GameController.instance.Bloodbath)
        {
            if(GameController.instance.EnemiesKilled >= 10)
            {
                GameController.instance.Bloodbath = true;
                EnemyBaseBehaviour.EnemyKilled -= CheckBloodbathAchievement;
                AchievementDisplay.instance.ShowPopup(GameController.Event.Bloodbath);
            }
        }
    }
}
