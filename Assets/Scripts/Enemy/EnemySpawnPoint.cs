using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> EnemyTypesToSpawn;

    [SerializeField]
    public List<float> EnemyTypesToSpawnSpawnRates;

    [SerializeField]
    public List<GameObject> BossTypesToSpawn;

    public float RandomSpawnPositionRadius = 3f;
    // Start is called before the first frame update
    void Start()
    {
    }

    public GameObject GetEnemyTypeToSpawn()
    {
        GameObject typeSelected = null;
        float value = Random.Range(0f, 1f);
        for(int i = 0; i < EnemyTypesToSpawn.Count; i++)
        {
            var eType = EnemyTypesToSpawn[i];
            var probability = EnemyTypesToSpawnSpawnRates[i];
            //The list must have the rarest piece at the top
            if(probability >= value || i == EnemyTypesToSpawnSpawnRates.Count-1)
            {
                typeSelected = eType;
                break;
            }
        }

        return typeSelected;
    }

    public GameObject GetBossTypeToSpawn()
    {
        if(BossTypesToSpawn.Count == 0)
        {
            return null;
        }

        GameObject typeSelected = null;
        int index = Random.RandomRange(0, BossTypesToSpawn.Count-1);
        var eType = BossTypesToSpawn[index];            

        return eType;
    }

    public Boss SpawnBoss(Transform target)
    {
        GameObject typeToSpawn = GetBossTypeToSpawn();

        Vector3 loc = gameObject.transform.position;

        Vector3 randomVec = Vector3.one;
        randomVec.y = 0;
        randomVec.Scale(Random.insideUnitSphere * RandomSpawnPositionRadius);
        Vector3 finalPos = loc + randomVec;

        var enemy = EnemySpawner.SpawnEnemy(finalPos, typeToSpawn, target);
        if(enemy is Boss boss)
        {
            return boss;
        }
        Debug.LogError("The spawned type is not a boss");
        return null;
    }

    public Enemy SpawnBasicEnemy(Transform target)
    {
        GameObject typeToSpawn = GetEnemyTypeToSpawn();
        Vector3 loc = gameObject.transform.position;

        Vector3 randomVec = Vector3.one;
        randomVec.y = 0;
        randomVec.Scale(Random.insideUnitSphere * RandomSpawnPositionRadius);

        Vector3 finalPos = loc + randomVec;

        return EnemySpawner.SpawnEnemy(finalPos, typeToSpawn, target);
    }
}