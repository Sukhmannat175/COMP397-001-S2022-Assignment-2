/*  Filename:           GameOverScreen.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 18, 2022
 *  Description:        Load Game Over Screen.
 *  Revision History:   June 18, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : PauseScreen
{
    public enum GameEndState
    {
        VICTORY,
        GAMEOVER,
    }

    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private Text titleLabel;
    [SerializeField] private Text scoreLabel;
    [SerializeField] private Text enemiesKilledLabel;
    [SerializeField] private string victoryTitle;
    [SerializeField] private string gameOverTitle;

    public void Open(GameEndState state, int score, int enemiesKilled)
    {
        Open();
        scoreLabel.text = score.ToString();
        enemiesKilledLabel.text = enemiesKilled.ToString();
        healthDisplay.Init(PlayerHealthBarController.instance.MaxHealthValue);
        healthDisplay.SetHealthValue(PlayerHealthBarController.instance.CurrentHealthValue);

        switch (state)
        {
            case GameEndState.GAMEOVER:
                titleLabel.text = gameOverTitle;
                break;
            case GameEndState.VICTORY:
                titleLabel.text = victoryTitle;
                break;
            default:
                Debug.LogError(state + " does not support by code.");
                break;
        }
    }
}
