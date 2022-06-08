// CollapseButton.cs
// Yuk Yee Wong - 301234795
// 06/06/2022
// Collapse Button
// Initial Script

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
