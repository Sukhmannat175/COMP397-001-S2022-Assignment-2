/*  Filename:           CollapseButton.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 6, 2022
 *  Description:        Collapse Button.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class CollapseButton : MonoBehaviour
{
    [SerializeField] private Transform buttonImageTransform;
    [SerializeField] private GameObject expandedPart;

    public void OnButtonClick()
    {
        buttonImageTransform.localRotation = Quaternion.Euler(buttonImageTransform.localRotation.x, buttonImageTransform.localRotation.y, expandedPart.activeSelf ? 0 : 180);
        expandedPart.SetActive(!expandedPart.activeSelf);
    }
}
