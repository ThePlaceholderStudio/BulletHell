using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField]
    public GameObject EnemyTypeToSpawn;

    public float RandomSpawnPositionRadius = 3f;
    // Start is called before the first frame update
    void Start()
    {
    }

    public Enemy SpawnEnemy(Transform target)
    {
        var go = GameObject.Instantiate(EnemyTypeToSpawn);

        Vector3 loc = gameObject.transform.position;

        Vector3 randomVec = Vector3.one;
        randomVec.y = 0;
        randomVec.Scale(Random.insideUnitSphere * RandomSpawnPositionRadius);


        Vector3 finalPos = loc + randomVec;

        go.transform.position = finalPos;

        var enemyComponent = go.GetComponent<Enemy>();

        var movementComponent = go.GetComponent<NavMeshMovement>();
        if(movementComponent != null)
        {
            movementComponent.SetTarget(target);
        }
        return enemyComponent;
    }
}