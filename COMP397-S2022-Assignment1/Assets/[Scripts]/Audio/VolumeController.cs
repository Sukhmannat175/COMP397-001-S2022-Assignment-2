/*  Filename:           VolumeController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 6, 2022
 *  Description:        Volume controller.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeController : MonoBehaviour
{
    [SerializeField] private SoundManager.SoundType type;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        UpdateInitialVolume();
        slider.onValueChanged.AddListener(OnValueChange);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnValueChange);
    }

    void UpdateInitialVolume()
    {
        switch (type)
        {
            case SoundManager.SoundType.MUSIC:
                slider.value = SoundManager.instance.MusicVolume;
                break;
            case SoundManager.SoundType.SFX:
                slider.value = SoundManager.instance.SfxVolume;
                break;
            default:
                Debug.LogError("Please update the sound type in the slider.");
                break;
        }
    }

    void OnValueChange(float value)
    {
        SoundManager.instance.SetVolume(value, type);
    }
}
