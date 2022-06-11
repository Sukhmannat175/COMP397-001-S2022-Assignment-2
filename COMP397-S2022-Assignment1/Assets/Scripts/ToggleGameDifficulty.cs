/*  Filename:       ToggleGameDifficulty.cs
 *  Author:         Marcus Ngooi (301147411)
 *  Last Update:    June 11, 2022
 *  Description:    Communictes with GameDifficultyManager based on selections in options screen.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleGameDifficulty : MonoBehaviour
{
    [SerializeField] private Text difficultyLabel;
    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        toggle.onValueChanged.AddListener(onValueChanged);
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(onValueChanged);
    }

    public void onValueChanged(bool value)
    {
        if (GameDifficultyManager.instance != null)
        {
            switch (difficultyLabel.text.ToUpper())
            {
                case "EASY":
                    GameDifficultyManager.instance.setEasyGameDifficulty();
                    break;
                case "NORMAL":
                    GameDifficultyManager.instance.setNormalGameDifficulty();
                    break;
                case "DIFFICULT":
                    GameDifficultyManager.instance.setDifficultGameDifficulty();
                    break;
            }
        }
    }
}
