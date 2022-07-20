/*  Filename:           RuntimePlatformHelper.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        July 20, 2022
 *  Description:        Runtime Platform Helper.
 *  Revision History:   July 20, 2022 (Yuk Yee Wong): Initial script.
 *  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimePlatformHelper : MonoBehaviour
{
    [Header("Show and Hide")]
    [SerializeField] private bool show;
    [SerializeField] private List<RuntimePlatform> platforms;

    private bool forceMobileLayout = false;

    void Start()
    {
        if (show)
        {
            if (!forceMobileLayout)
                gameObject.SetActive(platforms.Contains(Application.platform));
            else
                gameObject.SetActive(!platforms.Contains(Application.platform));
        }
        else
        {
            if (!forceMobileLayout)
                gameObject.SetActive(!platforms.Contains(Application.platform));
            else
                gameObject.SetActive(platforms.Contains(Application.platform));
        }
    }
}
