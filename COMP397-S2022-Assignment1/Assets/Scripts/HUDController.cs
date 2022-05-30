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
