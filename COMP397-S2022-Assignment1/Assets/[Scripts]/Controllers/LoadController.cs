/*  Filename:           LoadController.cs
 *  Author:             Sukhmannat Singh (301168420)
 *  Last Update:        August 05, 2022
 *  Description:        Controls loading of the game from main menu
 *  Revision History:   August 05, 2022 (Sukhmannat Singh): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LoadController : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject loadDataButton1;
    [SerializeField] private GameObject loadDataButton2;

    private string fileName;
    private bool sceneChange = false;
    void Update()
    {
        DontDestroyOnLoad(gameObject);
        if (SceneManager.GetActiveScene().name == "Main" && sceneChange)
        {
            Debug.Log(1);
            GameController.instance.OnLoad(fileName);
            sceneChange = false;
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PreLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/saves/Save 1.save"))
        {
            SaveData.current = (SaveData)SerializationController.Load(Application.persistentDataPath + "/saves/Save 1.save");

            loadDataButton1.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Save 1";
            loadDataButton1.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Wave: " + SaveData.current.playerData.wave;
            loadDataButton1.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Player Health: " + SaveData.current.playerData.health;
            loadDataButton1.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Score: " + SaveData.current.playerData.score;
        }

        if (File.Exists(Application.persistentDataPath + "/saves/Save 2.save"))
        {
            SaveData.current = (SaveData)SerializationController.Load(Application.persistentDataPath + "/saves/Save 2.save");

            loadDataButton2.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Save 2";
            loadDataButton2.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Player Health: " + SaveData.current.playerData.health;
            loadDataButton2.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Wave: " + SaveData.current.playerData.wave;
            loadDataButton2.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Score: " + SaveData.current.playerData.score;
        }
    }

    public void OnLoad(string file)
    {
        sceneChange = true;
        fileName = file;
        Debug.Log(fileName);
    }
}
