// ChangeMusicHelper.cs
// Yuk Yee Wong - 301234795
// 06/06/2022
// Change Music Helper
// Initial Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicHelper : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    private void OnEnable()
    {
        Play();
    }

    public void Play()
    {
        SoundManager.instance.ChangeMusic(clip);
    }
}
