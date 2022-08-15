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
    // Singleton
    public static AchievementManager instance;

    // Private variables
    [SerializeField] private bool firstTower = false;

    // Properties

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
