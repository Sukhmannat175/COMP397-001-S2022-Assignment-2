/*  Filename:       HUDController.cs
 *  Author:         Marcus Ngooi (301147411)
                    Yuk Yee Wong (301234795)
 *  Last Update:    June 11, 2022
 *  Description:    Toggles pause screen.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private PauseScreen pauseScreen;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pauseScreen.gameObject.activeInHierarchy)
            {
                pauseScreen.Close();
                SoundManager.instance.ChangeMusic(GetComponent<ChangeMusicHelper>().clip);
            }    
            else
            {
                pauseScreen.Open();
            }
        }

    }
}
