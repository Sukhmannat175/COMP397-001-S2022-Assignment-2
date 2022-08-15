/*  Filename:           GameController.cs
 *  Author:             Sukhmannat Singh (301168420)
 *  Last Update:        August 1, 2022
 *  Description:        Controls the tutorial window.
 *  Revision History:   June 11, 2022 (Sukhmannat Singh): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtInstructions;
    [SerializeField] private Image imgArrow;
    [SerializeField] private AudioClip playClip;
    [SerializeField] private CollapseButton collapseButton;

    public static TutorialController instance;
    
    public TutorialState state;
    private bool continueGame = false;
    private int currentStep = 1;
    private int nextStep = 0;
    public enum TutorialState
    {
        TUTORIAL,
        GAMEPLAY,
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.EnemiesSpawned == 3 && currentStep == 1 && Time.timeScale == 1)
        {
            if (state == TutorialState.TUTORIAL)
            {
                PlayTowerSelectTutorial(1);
            }
        }

        if (GameController.instance.CurrentWave == 2 && currentStep == 2 && Time.timeScale == 1)
        {
            if (state == TutorialState.TUTORIAL)
            {
                PlayTowerSelectTutorial(3);
            }
        }

        if (GameController.instance.EnemiesSpawned == 5 && currentStep == 8 && Time.timeScale == 1)
        {
            if (state == TutorialState.TUTORIAL)
            {
                PlayTowerSelectTutorial(9);
            }
        }
    }

    public void OnBtnClickContinue()
    {
        if (continueGame == true)
        {
            this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 600, 0);
            continueGame = false;
            Time.timeScale = 1;
        }
        else
        {
            PlayTowerSelectTutorial(nextStep);
        }
    }

    public void PlayTowerSelectTutorial(int step)
    {
        switch (step)
        {
            case 1:
                imgArrow.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 90);
                imgArrow.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 108, 0);
                this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(585, 80, 0);
                txtInstructions.text = "Enemies are spawned in waves and every wave is unique...";
                SoundManager.instance.PlaySFX(playClip);
                nextStep = 2;
                currentStep = step;
                Time.timeScale = 0;
                break;

            case 2:
                txtInstructions.text = "...This is the Grunt Golem. It will keep walking until it reaches the end of the path.";
                currentStep = step;
                continueGame = true;
                break;

            case 3:
                imgArrow.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 270);
                imgArrow.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, -108, 0);
                this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(1001, -100, 0);
                txtInstructions.text = "Towers kill enemies and prevent them from reaching the end. Select towers from this window...";
                SoundManager.instance.PlaySFX(playClip);
                nextStep = 4;
                currentStep = step;
                Time.timeScale = 0;
                break;

            case 4:
                this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(1001, 33, 0);
                txtInstructions.text = "...The dropdown button reveals the name, cost, and description of the tower...";
                collapseButton.OnButtonClick();
                currentStep = step;
                nextStep = 5;
                break;

            case 5:
                imgArrow.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-110, -108, 0);
                txtInstructions.text = "...The Crossbow Tower shoots a single target with high attack speed...";
                currentStep = step;
                nextStep = 6;
                break;

            case 6:
                imgArrow.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, -108, 0);
                txtInstructions.text = "...The Cannon Tower does splash damage with low attack speed...";
                currentStep = step;
                nextStep = 7;
                break;

            case 7:
                imgArrow.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(125, -108, 0);
                txtInstructions.text = "...The Resource Tower does not attack enemies, instead it gives you resources...";
                currentStep = step;
                nextStep = 8;
                break;

            case 8:
                this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(1001, -100, 0);
                txtInstructions.text = "Tip: Start with a Crossbow Tower and then a Resource Tower.";
                collapseButton.OnButtonClick();
                currentStep = step;
                continueGame = true;
                break;

            case 9:
                imgArrow.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 90);
                imgArrow.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 108, 0);
                this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(585, 80, 0);
                txtInstructions.text = "This is the Resource Stealer. It can go invincible periodically and steal resources...";
                SoundManager.instance.PlaySFX(playClip);
                nextStep = 2;
                currentStep = step;
                Time.timeScale = 0;
                break;

            case 10:
                txtInstructions.text = "...It only steals resource in its invincible state, and surfaces after small intervals.";
                currentStep = step;
                continueGame = true;
                break;
        }
        
    }
}
