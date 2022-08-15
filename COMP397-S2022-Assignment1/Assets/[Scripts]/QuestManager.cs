/*  Filename:           QuestManager.cs
 *  Author:             Marcus Ngooi (301147411)
 *  Last Update:        August 14, 2022
 *  Description:        For quests.
 *  Revision History:   August 14, 2022 (Marcus ngooi): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    // "Public" variables
    [SerializeField] private TextMeshProUGUI firstTowerText;
    [SerializeField] private TextMeshProUGUI lastTowerText;
    [SerializeField] private TextMeshProUGUI firstBloodText;
    [SerializeField] private TextMeshProUGUI bloodbathText;

    // Singleton
    public static QuestManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //subscribe to all events that are required
        TowerPlacer.FirstTowerBuilt += CheckFirstTowerQuest;
        TowerPlacer.LastTowerBuilt += CheckLastTowerQuest;
        EnemyBaseBehaviour.EnemyKilled += CheckFirstBloodQuest;
        EnemyBaseBehaviour.EnemyKilled += CheckBloodbathQuest;
    }

    public void RefreshQuestStatus()
    {
        CheckFirstTowerQuest();
        CheckLastTowerQuest();
        CheckFirstBloodQuest();
        CheckBloodbathQuest();
    }

    private void CheckFirstTowerQuest()
    {
        if (GameController.instance.FirstTower)
        {            
            GameController.instance.FirstTower = true;
            //unsubscribe from this so that we don't need to keep listening for it
            TowerPlacer.FirstTowerBuilt -= CheckFirstTowerQuest;
            firstTowerText.fontStyle = FontStyles.Strikethrough;
        }
    }

    private void CheckLastTowerQuest()
    {
        if (GameController.instance.LastTower)
        {
            GameController.instance.LastTower = true;
            TowerPlacer.LastTowerBuilt -= CheckLastTowerQuest;
            lastTowerText.fontStyle = FontStyles.Strikethrough;
        }
    }

    private void CheckFirstBloodQuest()
    {
        if (GameController.instance.FirstBlood)
        {
            GameController.instance.FirstBlood = true;
            EnemyBaseBehaviour.EnemyKilled -= CheckFirstBloodQuest;
            firstBloodText.fontStyle = FontStyles.Strikethrough;
        }
    }

    private void CheckBloodbathQuest()
    {
        if (GameController.instance.Bloodbath)
        {
            if (GameController.instance.EnemiesKilled >= 10)
            {
                GameController.instance.Bloodbath = true;
                EnemyBaseBehaviour.EnemyKilled -= CheckBloodbathQuest;
                bloodbathText.fontStyle = FontStyles.Strikethrough;
            }
        }
    }
}
