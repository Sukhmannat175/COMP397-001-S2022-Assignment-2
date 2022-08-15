/*  Filename:           ExitGameHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        May 29, 2022
 *  Description:        Exit the game.
 *  Revision History:   May 29, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameHelper : MonoBehaviour
{
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
