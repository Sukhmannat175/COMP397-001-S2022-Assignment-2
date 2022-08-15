/*  Filename:           AchievementDisplay.cs
 *  Author:             Han Bi (301176547)
 *                      Marcus Ngooi (301147411)
 *  Last Update:        August 14, 2022
 *  Description:        For placing towers.
 *  Revision History:   August 8, 2022 (Han Bi): Initial script.
 *                      August 14, 2022 (Marcus Ngooi): Shifting events to the GameController.cs to generalize first blood, first tower, etc. to achievements, quests, tutorial.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour
{
    [SerializeField]
    int timeOnScreen = 5;

    [SerializeField]
    GameObject popUp;
    
    [SerializeField]
    GameObject popupTitle;

    [SerializeField]
    GameObject popupImage;

    [SerializeField]
    GameObject popupDesc;


    [SerializeField]
    List<string> achievementTitle = new List<string>();

    [SerializeField]
    List<string> achievementDesc = new List<string>();

    [SerializeField]
    List<Texture> achievementIcon = new List<Texture>();

    [SerializeField] private AudioClip playSound;
    // Start is called before the first frame update

    public static AchievementDisplay instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        popUp.SetActive(false);
    }


    public void ShowPopup(GameController.Event achievement)
    {
        SoundManager.instance.PlaySFX(playSound);
        StartCoroutine(CustomizePopup(achievement));
    }

    private IEnumerator CustomizePopup(GameController.Event achievement)
    {
        popUp.SetActive(true);
        UpdatePopup(achievement);        
        yield return new WaitForSeconds(timeOnScreen);
        popUp.SetActive(false);

    }

    private void UpdatePopup(GameController.Event achievement)
    {
        switch (achievement)
        {
            case GameController.Event.FirstTower:
                popupTitle.GetComponent<TMPro.TextMeshProUGUI>().text = achievementTitle[0];
                popupDesc.GetComponent<TMPro.TextMeshProUGUI>().text = achievementDesc[0];
                popupImage.GetComponent<RawImage>().texture = achievementIcon[0];
                break;

            case GameController.Event.LastTower:
                popupTitle.GetComponent<TMPro.TextMeshProUGUI>().text = achievementTitle[1];
                popupDesc.GetComponent<TMPro.TextMeshProUGUI>().text = achievementDesc[1];
                popupImage.GetComponent<RawImage>().texture = achievementIcon[1];
                break;

            case GameController.Event.FirstBlood:
                popupTitle.GetComponent<TMPro.TextMeshProUGUI>().text = achievementTitle[2];
                popupDesc.GetComponent<TMPro.TextMeshProUGUI>().text = achievementDesc[2];
                popupImage.GetComponent<RawImage>().texture = achievementIcon[2];
                break;

            case GameController.Event.Bloodbath:
                popupTitle.GetComponent<TMPro.TextMeshProUGUI>().text = achievementTitle[3];
                popupDesc.GetComponent<TMPro.TextMeshProUGUI>().text = achievementDesc[3];
                popupImage.GetComponent<RawImage>().texture = achievementIcon[3];
                break;
        }
    }
}
