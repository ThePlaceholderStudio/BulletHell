using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NavMeshMovement : MonoBehaviour
{
    private Transform targetPoint; // The target point to move towards

    private NavMeshAgent navMeshAgent;

    [SerializeField] int speed;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if(targetPoint != null)
        {
            navMeshAgent.SetDestination(targetPoint.position);
        }
    }

    internal void SetTarget(Transform target)
    {
        targetPoint = target;
    }
}
