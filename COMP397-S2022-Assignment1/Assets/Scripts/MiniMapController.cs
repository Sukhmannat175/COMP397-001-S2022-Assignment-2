using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float zOffset = 10.0f;
    [SerializeField] private float xOffset = 10.0f;

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
