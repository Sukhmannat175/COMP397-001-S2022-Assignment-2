/*  Filename:           LayoutChangeHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        July 20, 2022
 *  Description:        Layout Change Helper.
 *  Revision History:   July 20, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutChangeHelper : MonoBehaviour
{
    [SerializeField] private List<RuntimePlatform> platformsForJoystickLayout;

    [Header("Rect Transform Positional Change")]
    [SerializeField] private RectTransform desktopPosition;
    [SerializeField] private RectTransform leftJoystickPosition;
    [SerializeField] private RectTransform rightJoystickPosition;

    private RectTransform rectTransform;

    private bool forceMobileLayout = false;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();

        if (forceMobileLayout || platformsForJoystickLayout.Contains(Application.platform))
        {
            StartCoroutine(WaitForManager());
        }
        else
        {
            rectTransform.SetParent(desktopPosition);
            rectTransform.localPosition = Vector3.zero;
        }        
    }

    IEnumerator WaitForManager()
    {
        while (KeyBindingManager.instance == null)
            yield return new WaitForEndOfFrame();

        if (rectTransform != null)
        {
            if (KeyBindingManager.instance.SelectedNormalLayout)
            {
                rectTransform.SetParent(leftJoystickPosition);
                rectTransform.localPosition = Vector3.zero;
            }
            else
            {
                rectTransform.SetParent(rightJoystickPosition);
                rectTransform.localPosition = Vector3.zero;
            }
        }
    }
}
