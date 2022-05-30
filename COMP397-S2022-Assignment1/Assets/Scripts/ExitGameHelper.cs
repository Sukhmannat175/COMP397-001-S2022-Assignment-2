// ExitGameHelper.cs
// Yuk Yee Wong - 301234795
// 05/29/2022
// Exit the game
// Initial Script

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
