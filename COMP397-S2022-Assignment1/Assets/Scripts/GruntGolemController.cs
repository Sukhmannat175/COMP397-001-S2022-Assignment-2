using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GruntGolemController : MonoBehaviour
{
    [SerializeField] private Transform endPosition;

    private NavMeshAgent navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = endPosition.position;
    }
}
