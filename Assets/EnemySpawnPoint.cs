using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField]
    public GameObject EnemyTypeToSpawn;

    public float RandomSpawnPositionRadius = 20f;
    // Start is called before the first frame update
    void Start()
    {
    }

    public Enemy SpawnEnemy()
    {
        var go = GameObject.Instantiate(EnemyTypeToSpawn);

        Vector3 loc = gameObject.transform.position;

        Vector3 randomVec = Vector3.one;
        randomVec.Scale(Random.insideUnitSphere * RandomSpawnPositionRadius);


        Vector3 finalPos = loc + randomVec;

        go.transform.position = finalPos;

        var enemyComponent = go.GetComponent<Enemy>();
        return enemyComponent;
    }
}