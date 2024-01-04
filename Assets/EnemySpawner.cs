using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    const float enemySpawnDistributionTime = 15; //This will be used to adjust enemy spawn rate. The code will spawn all the enemies of a wave in this amount of seconds.

    private List<EnemySpawnPoint> _enemySpawnPoints = new List<EnemySpawnPoint>();
    private Character _playerCharacter;

    private List<Enemy> _currentWaveEnemies = new List<Enemy>();

    private float TotalElapsedTime = 0f;

    private int Wave = 0;
    private float ElapsedTimeThisWave = 0f;

    private int WaveRemainingEnemyCount = 0;
    private float WaveEnemySpawnTime = 0f;
    private float WaveEnemySpawnTimeRemaining = 0f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var go in gameObject.scene.GetRootGameObjects())
        {
            
            var spawnPoint = go.GetComponent<EnemySpawnPoint>();
            if (spawnPoint != null)
            {
                _enemySpawnPoints.Add(spawnPoint);
            }

            var characterComponent = go.GetComponent<Character>();
            if (characterComponent != null)
            {
                _playerCharacter = characterComponent;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        TotalElapsedTime += Time.deltaTime;
        ElapsedTimeThisWave += Time.deltaTime;
        if(_enemySpawnPoints.Count <= 0 || _playerCharacter == null)
        {
            Debug.LogError($"EnemySpawnPoint count : {_enemySpawnPoints.Count} || PlayerCharacter : {_playerCharacter}");
        }

        if(WaveRemainingEnemyCount == 0 && ElapsedTimeThisWave > enemySpawnDistributionTime)
        {
            StartNewWave();
        }
        else if(WaveRemainingEnemyCount > 0)
        {
            TrySpawningEnemy();
        }
    }

    private void StartNewWave()
    {
        Wave++;
        
        ElapsedTimeThisWave = 0f;
        WaveRemainingEnemyCount = Wave * _playerCharacter.currentLevel * 15; //Find a better function for enemy counts

        WaveEnemySpawnTime = enemySpawnDistributionTime / WaveRemainingEnemyCount;

        Debug.Log($"Starting Wave : {Wave} | Enemy Count : {WaveRemainingEnemyCount} | Spawn Rate : Every {WaveEnemySpawnTime} seconds");
    }

    private void TrySpawningEnemy()
    {
        WaveEnemySpawnTimeRemaining -= Time.deltaTime;
        if (WaveEnemySpawnTimeRemaining <= 0 && WaveRemainingEnemyCount > 0)
        {
            WaveEnemySpawnTimeRemaining = WaveEnemySpawnTime;
            var index = Random.Range(0, _enemySpawnPoints.Count);
            var spawnPoint = _enemySpawnPoints[index];
            spawnPoint.SpawnEnemy();
            WaveRemainingEnemyCount--;
            Debug.Log($"SpawningEnemy, Will Spawn {WaveRemainingEnemyCount} more");
        }
    }
}
