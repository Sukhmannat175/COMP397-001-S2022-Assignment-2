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
    [SerializeField] private float cameraSpeed = 10.0f;

    // Private variables
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement based on KeyBinding Manager
        if (Input.GetKey(KeyBindingManager.instance.Up))
        {
            cameraTransform.position = new Vector3(cameraTransform.position.x,
                                                    cameraTransform.position.y,
                                                    cameraTransform.position.z + (1 * cameraSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyBindingManager.instance.Down))
        {
            cameraTransform.position = new Vector3(cameraTransform.position.x,
                                                    cameraTransform.position.y,
                                                    cameraTransform.position.z + (-1 * cameraSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyBindingManager.instance.Left))
        {
            cameraTransform.position = new Vector3(cameraTransform.position.x + (-1 * cameraSpeed * Time.deltaTime),
                                                    cameraTransform.position.y,
                                                    cameraTransform.position.z);
        }
        if (Input.GetKey(KeyBindingManager.instance.Right))
        {
            cameraTransform.position = new Vector3(cameraTransform.position.x + (1 * cameraSpeed * Time.deltaTime),
                                                    cameraTransform.position.y,
                                                    cameraTransform.position.z);
        }
    }
}
