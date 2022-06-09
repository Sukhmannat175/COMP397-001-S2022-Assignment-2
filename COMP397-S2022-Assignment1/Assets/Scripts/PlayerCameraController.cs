/*  Filename:       PlayerCameraController.cs
 *  Author:         Marcus Ngooi (301147411)
 *  Last Update:    June 9, 2022
 *  Description:    Allows player to move the camera around to see different parts of the map.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    // "Public" variables
    [Header("Movement")]
    [SerializeField] private float cameraSpeed = 0.5f;

    // Private variables
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        cameraTransform.position = new Vector3(cameraTransform.position.x + x * cameraSpeed,
                                                cameraTransform.position.y,
                                                cameraTransform.position.z + z * cameraSpeed);
    }
}
