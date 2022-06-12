/*  Filename:           PauseScreen.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        May 29, 2022
 *  Description:        Load Pause Screen.
 *  Revision History:   May 29, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
