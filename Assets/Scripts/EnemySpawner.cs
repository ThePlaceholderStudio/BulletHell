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

    private List<Enemy> CurrentWaveEnemies = new List<Enemy>();

    public float TotalElapsedTime = 0f;

    public int Wave = 0;
    public float ElapsedTimeThisWave = enemySpawnDistributionTime;

    public int WaveRemainingEnemyCountToSpawn = 0;
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
        }
    }

    private void TrySetPlayer()
    {
        if(_playerCharacter != null)
        {
            return;
        }

        foreach (var go in gameObject.scene.GetRootGameObjects())
        {
            var characterComponent = go.GetComponent<Character>();
            if (characterComponent != null)
            {
                _playerCharacter = characterComponent;
            }
            else
            {
                characterComponent = go.GetComponentInChildren<Character>();
                if (characterComponent != null)
                {
                    _playerCharacter = characterComponent;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        TrySetPlayer();

        TotalElapsedTime += Time.deltaTime;
        ElapsedTimeThisWave += Time.deltaTime;
        if(_enemySpawnPoints.Count <= 0 || _playerCharacter == null)
        {
            Debug.LogError($"EnemySpawnPoint count : {_enemySpawnPoints.Count} || PlayerCharacter : {_playerCharacter}");
        }

        if(WaveRemainingEnemyCountToSpawn == 0 && CurrentWaveEnemies.Count == 0 && ElapsedTimeThisWave > enemySpawnDistributionTime)
        {
            StartNewWave();
        }
        else if(WaveRemainingEnemyCountToSpawn > 0)
        {
            TrySpawningEnemy();
        }
    }

    private void StartNewWave()
    {
        Wave++;
        
        ElapsedTimeThisWave = 0f;
        WaveRemainingEnemyCountToSpawn = Wave * (1+((int)Math.Log(_playerCharacter.currentLevel+1, 2))) * 5; //Find a better function for enemy counts

        WaveEnemySpawnTime = enemySpawnDistributionTime / WaveRemainingEnemyCountToSpawn;

        Debug.Log($"Starting Wave : {Wave} | Enemy Count : {WaveRemainingEnemyCountToSpawn} | Spawn Rate : Every {WaveEnemySpawnTime} seconds");
    }

    private void TrySpawningEnemy()
    {
        WaveEnemySpawnTimeRemaining -= Time.deltaTime;
        if (WaveEnemySpawnTimeRemaining <= 0 && WaveRemainingEnemyCountToSpawn > 0 && _enemySpawnPoints.Count > 0)
        {
            WaveEnemySpawnTimeRemaining = WaveEnemySpawnTime;
            var index = Random.Range(0, _enemySpawnPoints.Count);
            var spawnPoint = _enemySpawnPoints[index];
            var spawnedEnemy = spawnPoint.SpawnEnemy(_playerCharacter.transform);
            Enemy enemyComponent = spawnedEnemy.GetComponent<Enemy>();

            enemyComponent.onEnemyKilled = OnEnemyKilled;

            CurrentWaveEnemies.Add(spawnedEnemy);
            WaveRemainingEnemyCountToSpawn--;
            Debug.Log($"SpawningEnemy, Will Spawn {WaveRemainingEnemyCountToSpawn} more");
        }
    }

    public void OnEnemyKilled(Enemy enemy)
    {
        CurrentWaveEnemies.Remove(enemy);
    }
}
