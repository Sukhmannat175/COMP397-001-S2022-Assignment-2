/*  Filename:           SoundManager.cs
 *  Author:             Yuk Yee Wong (301234795)
 *                      Marcus Ngooi (301147411)
 *  Last Update:        June 12, 2022
 *  Description:        Manages sound.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script.
 *                      June 12, 2022 (Marcus Ngooi): Added Start function to reduce volume to 0.5 on start starting line 46. 
 */

using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public enum SoundType { NONE = 0, MUSIC = 1, SFX = 2 }

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioSource buttonAudioSource;
    [SerializeField] AudioSource enemyDeathAudioSource;
    [SerializeField] AudioSource collectResourcesAudioSource;
    [SerializeField] AudioSource towerDestroyAudioSource;
    [SerializeField] AudioSource playerDamageAudioSource;

    [Header("Debug")]
    [SerializeField] private float musicVolume = 0.5f;
    [SerializeField] private float sfxVolume = 0.5f;

    public const string MusicParameter = "MusicVol";
    public const string SfxParameter = "SfxVol";
    private const float setVolumeMultiplier = 20f;

    public static SoundManager instance;

    public float MusicVolume { get { return musicVolume; } }

    public float SfxVolume { get { return sfxVolume; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetVolume(0.5f, SoundType.MUSIC);
        SetVolume(0.5f, SoundType.SFX);
    }

    public void ChangeMusic(AudioClip clip)
    {
        if (!musicAudioSource.clip.name.Equals(clip.name))
            musicAudioSource.clip = clip;

        musicAudioSource.Play();
    }

    public void PlayButtonSfx()
    {
        buttonAudioSource.Play();
    }

    public void PlayEnemyDeathSfx()
    {
        enemyDeathAudioSource.Play();
    }

    public void PlayCollectResourcesSfx()
    {
        collectResourcesAudioSource.Play();
    }

    public void PlayTowerDestroySfx()
    {
        towerDestroyAudioSource.Play();
    }

    public void PlayPlayerDamageSfx()
    {
        playerDamageAudioSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip); 
    }

    public void SetVolume(float value, SoundType type)
    {
        float newValue = Mathf.Log10(value) * setVolumeMultiplier;

        if (value == 0)
        {
            newValue = -100;
        }

        switch (type)
        {
            case SoundType.MUSIC:
                musicVolume = value;
                audioMixer.SetFloat(MusicParameter, newValue);
                break;
            case SoundType.SFX:
                sfxVolume = value;
                audioMixer.SetFloat(SfxParameter, newValue);
                break;
            default:
                Debug.LogError("Please assign the sound type before setting volume");
                break;
        }
    }
}
