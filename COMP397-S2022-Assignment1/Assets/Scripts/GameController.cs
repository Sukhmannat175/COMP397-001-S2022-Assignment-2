using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private GruntGolemController gruntGolemController;

    [Header("Enemies")]
    [SerializeField] private GameObject gruntGolem;
    [SerializeField] private GameObject WayPoints;
    [SerializeField] private Transform gruntGolemSpawn;

    private bool spawn = true;

    private IEnumerator Spawn()
    {
        while (spawn)
        {
            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForSeconds(1);
                Instantiate(gruntGolem, gruntGolemSpawn.localPosition, gruntGolem.transform.localRotation);
            }
            spawn = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gruntGolemController = gruntGolem.GetComponent<GruntGolemController>();
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gruntGolemController.wayPoints.Length; i++)
        {
            gruntGolemController.wayPoints[i] = WayPoints.transform.GetChild(i);
        }
    }
}
