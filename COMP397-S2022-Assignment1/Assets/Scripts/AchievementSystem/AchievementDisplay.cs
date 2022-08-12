/*  Filename:           AchievementDisplay.cs
 *  Author:             Han Bi (301176547)
 *  Last Update:        August 8, 2022
 *  Description:        For placing towers.
 *  Revision History:   August 8, 2022 (Han Bi): Initial script.
 *  
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


    public void ShowPopup(AchievementManager.Achievement achievement)
    {
        StartCoroutine(CustomizePopup(achievement));
    }

    private IEnumerator CustomizePopup(AchievementManager.Achievement achievement)
    {
        popUp.SetActive(true);
        UpdatePopup(achievement);        
        yield return new WaitForSeconds(timeOnScreen);
        popUp.SetActive(false);

    }

    private void UpdatePopup(AchievementManager.Achievement achievement)
    {
        switch (achievement)
        {
            case AchievementManager.Achievement.FirstTower:
                popupTitle.GetComponent<TMPro.TextMeshProUGUI>().text = achievementTitle[0];
                popupDesc.GetComponent<TMPro.TextMeshProUGUI>().text = achievementDesc[0];
                popupImage.GetComponent<RawImage>().texture = achievementIcon[0];
                break;

            case AchievementManager.Achievement.LastTower:
                popupTitle.GetComponent<TMPro.TextMeshProUGUI>().text = achievementTitle[1];
                popupDesc.GetComponent<TMPro.TextMeshProUGUI>().text = achievementDesc[1];
                popupImage.GetComponent<RawImage>().texture = achievementIcon[1];
                break;

            case AchievementManager.Achievement.FirstBlood:
                popupTitle.GetComponent<TMPro.TextMeshProUGUI>().text = achievementTitle[2];
                popupDesc.GetComponent<TMPro.TextMeshProUGUI>().text = achievementDesc[2];
                popupImage.GetComponent<RawImage>().texture = achievementIcon[2];
                break;

            case AchievementManager.Achievement.Bloodbath:
                popupTitle.GetComponent<TMPro.TextMeshProUGUI>().text = achievementTitle[3];
                popupDesc.GetComponent<TMPro.TextMeshProUGUI>().text = achievementDesc[3];
                popupImage.GetComponent<RawImage>().texture = achievementIcon[3];
                break;

                
        }

    }
}
