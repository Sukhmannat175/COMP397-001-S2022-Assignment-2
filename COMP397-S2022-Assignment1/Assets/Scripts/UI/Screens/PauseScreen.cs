/*  Filename:           PauseScreen.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        May 29, 2022
 *  Description:        Load Pause Screen.
 *  Revision History:   May 29, 2022 (Yuk Yee Wong): Initial script.
 *                      June 18, 2022 (Yuk Yee Wong): Update Time.timeScale in OnEnable and onDisable instead of in Open and Close.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
