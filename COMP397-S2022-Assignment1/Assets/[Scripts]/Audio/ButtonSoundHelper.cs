/*  Filename:           ButtonSoundHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 6, 2022
 *  Description:        Button Sound Helper.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class ButtonSoundHelper : MonoBehaviour
{
    public void PlaySfx()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.PlayButtonSfx();
    }
}
