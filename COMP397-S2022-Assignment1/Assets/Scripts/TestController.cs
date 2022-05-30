// TestController.cs
// Charlie Han Bi - 301176547
// 05/29/2022
// Load Game Over Screen
// Initial Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestController : MonoBehaviour
{

    public GameObject inGameMenu;
    private bool menuActive;
    // Start is called before the first frame update
    void Start()
    {
        menuActive = false;
        inGameMenu.SetActive(menuActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("GameOver");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuActive = !menuActive;
            inGameMenu.SetActive(menuActive);

        }
    }
}
