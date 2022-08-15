/*  Filename:           ToggleHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 6, 2022
 *  Description:        Toggle Helper.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script.
 */

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
