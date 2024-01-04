using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NavMeshMovement : MonoBehaviour
{
    private Transform targetPoint; // The target point to move towards

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        foreach(var go in gameObject.scene.GetRootGameObjects())
        {
            if(go.GetComponent<Character>() != null) //Character component should only be added to the player's object. So there will be no confusion
            {
                targetPoint = go.gameObject.transform;
                break;
            }
        }
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
}
