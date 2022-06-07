// SoundManager.cs
// Yuk Yee Wong - 301234795
// 06/06/2022
// Sound manager
// Initial Script

using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public enum SoundType { NONE = 0, MUSIC = 1, SFX = 2 }

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource buttonAudioSource;

    [Header("Debug")]
    [SerializeField] private float musicVolume = 1;
    [SerializeField] private float sfxVolume = 1;

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
