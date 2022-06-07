// ButtonSoundHelper.cs
// Yuk Yee Wong - 301234795
// 06/06/2022
// Button Sound Helper
// Initial Script

using UnityEngine;

public class ButtonSoundHelper : MonoBehaviour
{
    public void PlaySfx()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.PlayButtonSfx();
    }
}
