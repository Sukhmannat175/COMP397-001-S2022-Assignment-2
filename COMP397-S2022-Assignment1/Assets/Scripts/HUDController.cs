/*  Filename:           HUDController.cs
 *  Author:             Yuk Yee Wong (301234795)
                        Marcus Ngooi (301147411)
 *  Last Update:        June 11, 2022
 *  Description:        Toggles pause screen.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Intitial Script.
 *                      June 11, 2022 (Marcus Ngooi): Added change music logic on line 25.
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
