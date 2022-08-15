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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.EnemiesSpawned == 3 && currentStep == 1 && Time.timeScale == 1)
        {
            if (state == TutorialState.TUTORIAL)
            {
                PlayTowerSelectTutorial(currentStep);
            }
        }
    }

    public void OnBtnClickContinue()
    {
        if (continueGame == true)
        {
            this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 580, 0);
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
                imgArrow.gameObject.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 90, 90);
                imgArrow.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 108, 0);
                this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(585, 80, 0);
                txtInstructions.text = "Enemies are spawned in waves and every wave is unique...";
                nextStep = 2;
                currentStep = step;
                Time.timeScale = 0;
                break;

            case 2:
                imgArrow.gameObject.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 90, 90);
                imgArrow.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 108, 0);
                this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(585, 80, 0);
                txtInstructions.text = "...This is the Grunt Golem. It will keep walking until it reaches the end of the path.";
                currentStep = step;
                continueGame = true;
                Time.timeScale = 0;
                break;

            case 3:
                this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(1001, -100, 0);
                txtInstructions.text = "You can Select any Tower from this window";
                Time.timeScale = 0;
                break;

            case 4:
                break;
        }
        
    }
}
