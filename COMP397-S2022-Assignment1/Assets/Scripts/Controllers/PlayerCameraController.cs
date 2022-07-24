/*  Filename:           PlayerCameraController.cs
 *  Author:             Marcus Ngooi (301147411)
 *  Last Update:        July 21, 2022
 *  Description:        Camera Movement.
 *  Revision History:   June 9, 2022 (Marcus Ngooi): Allows player to move the camera around to see different parts of the level.

 *                      July 20, 2022 (Yuk Yee Wong): Add mobile touch functionality to control camera. 
 *                      July 21, 2022 (Yuk Yee Wong): Add camera movement restricts to control camera. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    // "Public" variables
    [SerializeField] private List<RuntimePlatform> platformsForJoystick;
    [SerializeField] private float sensitivity = 10.0f;
    [SerializeField] private Joystick joyStick;


    [Header("Limits of the camera movement")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY; 

    Vector3 vZero = Vector3.zero;


    // Private variables
    private Transform cameraTransform;
    private bool forceMobileLayout = false;

    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = 0;
        float verticalInput = 0;

        if (forceMobileLayout || platformsForJoystick.Contains(Application.platform))
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

            Vector3 posClamped = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
                                                   Mathf.Clamp(transform.position.y, minY, maxY),
                                                   Mathf.Clamp(transform.position.z, -43, 43));

            cameraTransform.position = posClamped; 
        };

    }
}
