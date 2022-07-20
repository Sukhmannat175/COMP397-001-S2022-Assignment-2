/*  Filename:           PlayerCameraController.cs
 *  Author:             Marcus Ngooi (301147411)
 *  Last Update:        June 9, 2022
 *  Description:        Button Sound Helper.
 *  Revision History:   June 9, 2022 (Marcus Ngooi): Allows player to move the camera around to see different parts of the level.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    // "Public" variables
    [SerializeField] private float sensitivity = 10.0f;
    [SerializeField] private Joystick joyStick;

    // Private variables
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = 0;
        float verticalInput = 0;

        if (Application.platform == RuntimePlatform.Android)
        {
            horizontalInput = KeyBindingManager.instance.SelectedNormalXAxis ? joyStick.Horizontal : -joyStick.Horizontal;
            verticalInput = KeyBindingManager.instance.SelectedNormalYAxis ? joyStick.Vertical : -joyStick.Vertical;
        }
        else
        {
            // Movement based on KeyBinding Manager
            if (Input.GetKey(KeyBindingManager.instance.Up))
            {
                verticalInput += 1;
            }
            if (Input.GetKey(KeyBindingManager.instance.Down))
            {
                verticalInput -= 1;
            }
            if (Input.GetKey(KeyBindingManager.instance.Left))
            {
                horizontalInput -= 1;
            }
            if (Input.GetKey(KeyBindingManager.instance.Right))
            {
                horizontalInput += 1;
            }
        }

        if (verticalInput != 0 || horizontalInput != 0)
        {
            cameraTransform.position = transform.position + new Vector3(horizontalInput, verticalInput, 0) * sensitivity * Time.deltaTime;
        }
    }
}
