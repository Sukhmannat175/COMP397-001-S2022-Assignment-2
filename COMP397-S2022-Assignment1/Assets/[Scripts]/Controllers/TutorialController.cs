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

    public enum TutorialState
    {
        TUTORIAL,
        GAMEPLAY,
    }

    // Start is called before the first frame update
    void Start()
    {
        state = TutorialState.TUTORIAL;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTowerSelectTutorial()
    {

    }
}
