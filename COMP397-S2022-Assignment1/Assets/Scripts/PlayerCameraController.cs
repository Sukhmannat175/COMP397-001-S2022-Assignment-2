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
    private KeyBindingManager keyBindingManager;

    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        keyBindingManager = GameObject.Find("KeyBindingManager").GetComponent<KeyBindingManager>();

        Debug.Log(keyBindingManager);
    }

    // Update is called once per frame
    void Update()
    {
        // Movement based on KeyBinding Manager
        if (Input.GetKey(keyBindingManager.Up))
        {            
            cameraTransform.position = new Vector3(cameraTransform.position.x, 
                                                    cameraTransform.position.y,
                                                    cameraTransform.position.z + (1 * cameraSpeed * Time.deltaTime));
        }
        if (Input.GetKey(keyBindingManager.Down))
        {            
            cameraTransform.position = new Vector3(cameraTransform.position.x,
                                                    cameraTransform.position.y,
                                                    cameraTransform.position.z + (-1 * cameraSpeed * Time.deltaTime));
        }
        if (Input.GetKey(keyBindingManager.Left))
        {
            cameraTransform.position = new Vector3(cameraTransform.position.x + (-1 * cameraSpeed * Time.deltaTime),
                                                    cameraTransform.position.y,
                                                    cameraTransform.position.z);
        }
        if (Input.GetKey(keyBindingManager.Right))
        {
            cameraTransform.position = new Vector3(cameraTransform.position.x + (1 * cameraSpeed * Time.deltaTime),
                                                    cameraTransform.position.y,
                                                    cameraTransform.position.z);
        }


        // Basic movement
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");

        //cameraTransform.position = new Vector3(cameraTransform.position.x + x * cameraSpeed,
        //                                        cameraTransform.position.y,
        //                                        cameraTransform.position.z + z * cameraSpeed);
    }
}
