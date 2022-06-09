// ToggleXYControls.cs
// Yuk Yee Wong - 301234795
// 06/06/2022
// Toggle X and Y Axis Controls
// Initial Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Apply to normal axis button
/// </summary>

[RequireComponent(typeof(Toggle))]
public class ToggleXYControls : MonoBehaviour
{
    public enum Axis { NONE = 0, X = 1, Y = 2 }
    [SerializeField] private Axis axis;
    [SerializeField] private List<KeyBindingHelper> keyBindingHelpers;
    
    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        if (KeyBindingManager.instance != null)
        {
            switch (axis)
            {
                case Axis.X:
                    toggle.isOn = KeyBindingManager.instance.SelectedNormalXAxis;
                    break;
                case Axis.Y:
                    toggle.isOn = KeyBindingManager.instance.SelectedNormalYAxis;
                    break;
                default:
                    Debug.LogError("Please select the axis");
                    break;
            }
        }

        toggle.onValueChanged.AddListener(OnValueChange);
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnValueChange);
    }

    public void OnValueChange(bool value)
    {
        if (KeyBindingManager.instance != null)
        {
            switch (axis)
            {
                case Axis.X:
                    if (value)
                        KeyBindingManager.instance.UseNormalXAxis();
                    else
                        KeyBindingManager.instance.UseInvertedXAxis();
                    break;
                case Axis.Y:
                    if (value)
                        KeyBindingManager.instance.UseNormalYAxis();
                    else
                        KeyBindingManager.instance.UseInvertedYAxis();
                    break;
                default:
                    Debug.LogError("Please select the axis");
                    break;
            }

            foreach (KeyBindingHelper helper in keyBindingHelpers)
                helper.UpdateKeyLabel();

        }
    }
}
