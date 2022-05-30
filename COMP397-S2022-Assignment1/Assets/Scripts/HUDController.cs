// HUDController.cs
// Yuk Yee Wong - 301234795
// 05/29/2022
// Toggle Pause Screen
// Initial Script


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
            }    
            else
            {
                pauseScreen.Open();
            }
        }

    }
}
