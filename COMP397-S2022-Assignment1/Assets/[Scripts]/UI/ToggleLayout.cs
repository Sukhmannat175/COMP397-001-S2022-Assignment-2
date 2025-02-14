/*  Filename:           ToggleLayout.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        July 20, 2022
 *  Description:        Toggle different mobile layouts.
 *  Revision History:   July 20, 2022 (Yuk Yee Wong): Initial script.
 *  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleLayout : MonoBehaviour
{
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
            toggle.isOn = !inverted ? 
                KeyBindingManager.instance.SelectedNormalLayout :
                !KeyBindingManager.instance.SelectedNormalLayout;
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
            if (value)
                KeyBindingManager.instance.UseNormalMobileLayout();
            else
                KeyBindingManager.instance.UseFlippedMobileLayout();
        }
    }
}
