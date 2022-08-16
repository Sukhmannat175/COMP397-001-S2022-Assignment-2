/*  Filename:           GameController.cs
 *  Author:             Sukhmannat Singh (301168420)
 *  Last Update:        August 1, 2022
 *  Description:        Controls the tutorial window.
 *  Revision History:   June 11, 2022 (Sukhmannat Singh): Initial script.
 *                      August 5, 2022 (Yuk Yee Wong): Fix position.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtInstructions;
    [SerializeField] private GameObject upArrow;
    [SerializeField] private GameObject downArrow;
    [SerializeField] private AudioClip playClip;
    [SerializeField] private CollapseButton collapseButton;
    [SerializeField] private GameObject container;

    [Header("Positions")]
    [SerializeField] private GameObject centerUpPosition;
    [SerializeField] private GameObject shopCollapsedPosition;
    [SerializeField] private GameObject shopExpandedPosition;
    [SerializeField] private GameObject enemyTilePosition;

    public static TutorialController instance;
    
    public TutorialState state;
    private bool continueGame = false;
    private bool towerPlaced = true;
    private int currentStep = 1;
    private int nextStep = 0;

    public enum TutorialState
    {
        TUTORIAL,
        GAMEPLAY,
    }

    private enum ArrowState
    {
        DISABLE = 0,
        UP = 1,
        DOWN = 2,
    }

    // Update is called once per frame
    void Update()
    {
        if (state == TutorialState.TUTORIAL && GameController.instance.EnemiesSpawned == 3 && currentStep == 1 && Time.timeScale == 1)
        {
            PlayTowerSelectTutorial(1);
        }

        if (state == TutorialState.TUTORIAL &&  GameController.instance.CurrentWave == 2 && currentStep == 2 && Time.timeScale == 1)
        {            
            PlayTowerSelectTutorial(3);
        }

        if (state == TutorialState.TUTORIAL &&  GameController.instance.EnemiesSpawned == 5 && currentStep == 8 && Time.timeScale == 1)
        {
            PlayTowerSelectTutorial(9);
        }

        if (state == TutorialState.TUTORIAL &&  GameController.instance.EnemiesSpawned == 9 && currentStep == 10 && Time.timeScale == 1)
        {
            PlayTowerSelectTutorial(11);
        }

        if (state == TutorialState.TUTORIAL && GameController.instance.TowersPlaced == 1 && Time.timeScale == 1 && towerPlaced)
        {
            PlayTowerSelectTutorial(0);
        }
    }

    public void OnBtnClickContinue()
    {
        if (continueGame == true)
        {
            container.SetActive(false);
            continueGame = false;
            Time.timeScale = 1;
        }
        else
        {
            PlayTowerSelectTutorial(nextStep);
        }
    }

    private void RepositionWithinScreenBound()
    {
        if (base.transform.position.x > Screen.width - 250f)
        {
            base.transform.position = new Vector3(Screen.width - 250f, base.transform.position.y, base.transform.position.z);
        }

        if (base.transform.position.y > Screen.height - 100f)
        {
            base.transform.position = new Vector3(base.transform.position.x, Screen.height - 100f, base.transform.position.z);
        }
    }

    private void ShowArrow(ArrowState state)
    {
        upArrow.SetActive(state == ArrowState.UP);
        downArrow.SetActive(state == ArrowState.DOWN);
    }

    public void OnBtnClickSkip()
    {
        container.SetActive(false);
        state = TutorialState.GAMEPLAY;
        Time.timeScale = 1;
    }

    public void PlayTowerSelectTutorial(int step)
    {
        container.SetActive(true);

        switch (step)
        {
            case 0:
                ShowArrow(ArrowState.DISABLE);
                transform.position = centerUpPosition.transform.position;
                txtInstructions.text = "Click on the complete button to finish the tower immediately for 100 gold";
                SoundManager.instance.PlaySFX(playClip);
                continueGame = true;
                towerPlaced = false;
                Time.timeScale = 0;
                break;

            case 1:
                ShowArrow(ArrowState.UP);
                transform.position = Camera.main.WorldToScreenPoint(enemyTilePosition.transform.position);
                txtInstructions.text = "Enemies are spawned in waves and every wave is unique...";
                SoundManager.instance.PlaySFX(playClip);
                nextStep = 2;
                currentStep = step;
                Time.timeScale = 0;
                break;

            case 2:
                txtInstructions.text = "...This is the Grunt Golem. It will keep walking until it reaches the end of the path.";
                SoundManager.instance.PlaySFX(playClip);
                currentStep = step;
                continueGame = true;
                break;

            case 3:
                ShowArrow(ArrowState.DOWN);
                transform.position = shopCollapsedPosition.transform.position;
                txtInstructions.text = "Towers kill enemies and prevent them from reaching the end. Select towers from this window...";
                SoundManager.instance.PlaySFX(playClip);
                nextStep = 4;
                currentStep = step;
                Time.timeScale = 0;
                break;

            case 4:
                transform.position = shopExpandedPosition.transform.position;
                txtInstructions.text = "...The dropdown button reveals the name, cost, and description of the tower...";
                collapseButton.OnButtonClick();
                SoundManager.instance.PlaySFX(playClip);
                currentStep = step;
                nextStep = 5;
                break;

            case 5:
                transform.position = shopCollapsedPosition.transform.position;
                txtInstructions.text = "...The Crossbow Tower shoots a single target with high attack speed...";
                SoundManager.instance.PlaySFX(playClip);
                collapseButton.OnButtonClick();
                currentStep = step;
                nextStep = 6;
                break;

            case 6:
                transform.position = shopCollapsedPosition.transform.position;
                txtInstructions.text = "...The Cannon Tower does splash damage with low attack speed...";
                SoundManager.instance.PlaySFX(playClip);
                currentStep = step;
                nextStep = 7;
                break;

            case 7:
                transform.position = shopCollapsedPosition.transform.position;
                txtInstructions.text = "...The Resource Tower does not attack enemies, instead it gives you Stone and Wood...";
                SoundManager.instance.PlaySFX(playClip);
                currentStep = step;
                nextStep = 8;
                break;

            case 8:
                transform.position = centerUpPosition.transform.position;
                ShowArrow(ArrowState.DISABLE);
                txtInstructions.text = "Tip: Start with a Crossbow Tower and then a Resource Tower.";                
                SoundManager.instance.PlaySFX(playClip);
                currentStep = step;
                continueGame = true;
                break;

            case 9:
                ShowArrow(ArrowState.UP);
                transform.position = Camera.main.WorldToScreenPoint(enemyTilePosition.transform.position);
                txtInstructions.text = "This is the Resource Stealer. It can go invincible periodically and steal resources...";
                SoundManager.instance.PlaySFX(playClip);
                nextStep = 10;
                currentStep = step;
                Time.timeScale = 0;
                break;

            case 10:
                txtInstructions.text = "...It only steals resource in its invincible state, and surfaces after small intervals.";
                SoundManager.instance.PlaySFX(playClip);
                currentStep = step;
                continueGame = true;
                break;

            case 11:
                ShowArrow(ArrowState.UP);
                transform.position = Camera.main.WorldToScreenPoint(enemyTilePosition.transform.position);
                txtInstructions.text = "This is the Tower Destroyer. It will attack towers until they are destroyed...";
                SoundManager.instance.PlaySFX(playClip);
                nextStep = 12;
                currentStep = step;
                Time.timeScale = 0;
                break;

            case 12:
                ShowArrow(ArrowState.DISABLE);
                transform.position = centerUpPosition.transform.position;
                txtInstructions.text = "Killing enemies gives you gold. You can also move the camera around using the Joystick/WASD keys...";
                SoundManager.instance.PlaySFX(playClip);
                nextStep = 13;
                currentStep = step;                
                break;

            case 13:
                txtInstructions.text = "This is Shadow's Pass. Your goal is to prevent the enemies from reaching the end. Good Luck!!!";
                SoundManager.instance.PlaySFX(playClip);
                currentStep = step;
                continueGame = true;
                state = TutorialState.GAMEPLAY;
                break;
        }
        
    }
}
