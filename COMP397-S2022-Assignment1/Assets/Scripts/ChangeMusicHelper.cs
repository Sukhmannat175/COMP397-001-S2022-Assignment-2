/*  Filename:           ChangeMusicHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 6, 2022
 *  Description:        Change Music Helper.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script.
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
        SoundManager.instance.ChangeMusic(clip);
    }
}
