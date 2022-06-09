// SaveLoadScreen.cs
// Yuk Yee Wong - 301234795
// 06/09/2022
// Save LoadS Screen
// Initial Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadScreen : PauseScreen
{
    [SerializeField] Text headingLabel;
    [SerializeField] Text descriptionLabel;
    [SerializeField] string saveText;
    [SerializeField] string saveDescriptionText;
    [SerializeField] string loadText;
    [SerializeField] string loadDescriptionText;

    public void OpenSaveScreen()
    {
        headingLabel.text = saveText;
        descriptionLabel.text = saveDescriptionText;
        Open();
    }
    public void OpenLoadScreen()
    {
        headingLabel.text = loadText;
        descriptionLabel.text = loadDescriptionText;
        Open();
    }
}
