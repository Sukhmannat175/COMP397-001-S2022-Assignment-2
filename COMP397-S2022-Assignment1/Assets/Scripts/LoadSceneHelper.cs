/*  Filename:           LoadSceneHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        May 29, 2022
 *  Description:        Load Scenes.
 *  Revision History:   May 29, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
