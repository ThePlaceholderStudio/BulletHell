using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBoss : Boss
{
    public const float MinionSpawnInterval = 4f;
    public float MinionSpawnTimer;

    public List<GameObject> EnemyTypesToSpawn = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        MinionSpawnTimer = MinionSpawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        MinionSpawnTimer -= Time.deltaTime;
        if(MinionSpawnTimer <= 0)
        {
            int index = Random.Range(0, EnemyTypesToSpawn.Count - 1);

            TrySpawningEnemy(EnemyTypesToSpawn[index]);
            MinionSpawnTimer = MinionSpawnInterval;
        }
    }

    private void TrySpawningEnemy(GameObject typeToSpawn)
    {
        if (typeToSpawn == null)
        {
            return;
        }

        var go = GameObject.Instantiate(typeToSpawn);

        Vector3 loc = gameObject.transform.position;

        Vector3 randomVec = Vector3.one;
        randomVec.y = 0;
        randomVec.Scale(Random.insideUnitSphere * 2);


        Vector3 finalPos = loc + randomVec;

        go.transform.position = finalPos;

        var enemyComponent = go.GetComponent<Enemy>();

        var movementComponent = go.GetComponent<NavMeshMovement>();
        if (movementComponent != null)
        {
            movementComponent.SetTarget(character.transform);
        }

        return;
    }
}
