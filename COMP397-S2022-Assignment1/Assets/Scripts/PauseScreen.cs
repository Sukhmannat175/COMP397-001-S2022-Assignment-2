// PauseScreen.cs
// Yuk Yee Wong - 301234795
// 05/29/2022
// Load Pause Screen
// Initial Script

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
