/*  Filename:           ToggleXYControls.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 6, 2022
 *  Description:        Toggle X and Y Axis Controls.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script.
 */

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
    [SerializeField] private bool inverted;
    
    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForManager());
    }

    IEnumerator WaitForManager()
    {
        while (KeyBindingManager.instance == null)
            yield return new WaitForEndOfFrame();

        if (KeyBindingManager.instance != null)
        {
            switch (axis)
            {
                case Axis.X:
                    toggle.isOn = !inverted ? 
                        KeyBindingManager.instance.SelectedNormalXAxis :
                        !KeyBindingManager.instance.SelectedNormalXAxis;
                    break;
                case Axis.Y:
                    toggle.isOn = !inverted ? 
                        KeyBindingManager.instance.SelectedNormalYAxis :
                        !KeyBindingManager.instance.SelectedNormalYAxis;
                    break;
                default:
                    Debug.LogError("Please select the axis");
                    break;
            }
        }

        if (!inverted)
        {
            toggle.onValueChanged.AddListener(OnValueChange);
        }
    }

    private void OnDisable()
    {
        if (!inverted)
        {
            toggle.onValueChanged.RemoveListener(OnValueChange);
        }
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
        }
    }
}
