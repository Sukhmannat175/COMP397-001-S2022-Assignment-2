// ToggleHelper.cs
// Yuk Yee Wong - 301234795
// 06/06/2022
// Toggle Helper
// Initial Script

using UnityEngine;

public class ToggleHelper : MonoBehaviour
{
    [SerializeField] private GameObject affectedGameObject;

    public void OnValueChange(bool value)
    {
        if (affectedGameObject != null)
            affectedGameObject.SetActive(value);
    }
}
