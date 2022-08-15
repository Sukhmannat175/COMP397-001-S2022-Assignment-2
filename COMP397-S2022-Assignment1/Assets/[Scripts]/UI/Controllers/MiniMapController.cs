/*  Filename:           MiniMapController.cs
 *  Author:             Marcus Ngooi (301147411)
 *  Last Update:        June 11, 2022
 *  Description:        Makes minimap camera follow player camera.
 *  Revision History:   June 11, 2022 (Marcus Ngooi): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float zOffset = 35.0f;
    [SerializeField] private float xOffset = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = FindObjectOfType<PlayerCameraController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerCamera.position.x + xOffset, transform.position.y, playerCamera.position.z + zOffset);
        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, playerCamera.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
