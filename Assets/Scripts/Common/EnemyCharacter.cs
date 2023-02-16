using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCharacter : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;
    private Transform target;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
    }

    
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //    if(Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        navMeshAgent.SetDestination(hit.point);
        //    }
        //}
        navMeshAgent.SetDestination(target.position);

    }
}
