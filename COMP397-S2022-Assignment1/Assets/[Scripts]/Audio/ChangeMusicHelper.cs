/*  Filename:           ChangeMusicHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        July 20, 2022
 *  Description:        Change Music Helper.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script.
 *                      July 20, 2022 (Yuk Yee Wong): Add coroutine to wait for sound manager.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicHelper : MonoBehaviour
{
    [SerializeField] public AudioClip clip;

    private void OnEnable()
    {
        Play();
    }

    public void Play()
    {
        StartCoroutine(WaitForManager());
    }

    IEnumerator WaitForManager()
    {
        while (SoundManager.instance == null)
        {
            yield return new WaitForEndOfFrame();
        }

        SoundManager.instance.ChangeMusic(clip);
    }
}
