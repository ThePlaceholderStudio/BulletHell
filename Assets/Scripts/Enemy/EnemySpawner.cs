using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;



public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    const float enemySpawnDistributionTime = 15; //This will be used to adjust enemy spawn rate. The code will spawn all the enemies of a wave in this amount of seconds.

    private List<EnemySpawnPoint> _enemySpawnPoints = new List<EnemySpawnPoint>();
    private Character _playerCharacter;

    private List<Enemy> CurrentWaveEnemies = new List<Enemy>();
    private List<Boss> CurrentWaveBosses = new List<Boss>();

    public float TotalElapsedTime = 0f;

    public int Wave = 0;
    //public float ElapsedTimeThisWave = enemySpawnDistributionTime;

    public int WaveRemainingEnemyCountToSpawn = 0;
    private float WaveEnemySpawnTime = 0f;
    private float WaveEnemySpawnTimeRemaining = 0f;
    public float BossSpawnWaveInterval = 1;

    public event Action<WaveInformation> OnWaveEnded = (WaveInformation info) => { };
    public event Action<Enemy> OnEnemyKilled = (Enemy enemy) => { };


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

        Instance = this;
        TotalWaveInformation = new WaveInformation(Instance, 0);
    }

    private void TrySetPlayer()
    {
        if (_playerCharacter != null)
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

        if(_playerCharacter != null)
        {
            StartNewWave();
        }
    }

    // Update is called once per frame
    void Update()
    {
        TrySetPlayer();

        if(_playerCharacter == null)
        {
            return;
        }

        UpdateWave();
    }


    private void UpdateWave()
    {
        if (SceneManager.GetActiveScene() != gameObject.scene)
        {
            return;
        }

        TotalElapsedTime += Time.deltaTime;
        CurrentWaveInformation.ElapsedTime += Time.deltaTime;
        if (_enemySpawnPoints.Count <= 0 || _playerCharacter == null)
        {
            Debug.LogError($"EnemySpawnPoint count : {_enemySpawnPoints.Count} || PlayerCharacter : {_playerCharacter}");
        }

        if (!_waitingWaveToStart && WaveRemainingEnemyCountToSpawn == 0 &&
            CurrentWaveEnemies.Count == 0 &&
            CurrentWaveBosses.Count == 0 &&
            CurrentWaveInformation.ElapsedTime > enemySpawnDistributionTime)
        {
            StartCoroutine(EndWave());
        }
        else if (WaveRemainingEnemyCountToSpawn > 0)
        {
            TrySpawningEnemy();
        }
    }

    private bool _waitingWaveToStart = false;
    private IEnumerator EndWave()
    {
        _waitingWaveToStart = true;
        OnWaveEnded?.Invoke(CurrentWaveInformation);

        yield return new WaitForSeconds(3f);
        StartNewWave();
        _waitingWaveToStart = false;
    }

    public WaveInformation CurrentWaveInformation;

    //TODO HAKAN: This should be in another class. Not in spawner
    public static WaveInformation TotalWaveInformation;

    private void SetNewWaveInformation()
    {
        if(CurrentWaveInformation != null)
        {
            TotalWaveInformation.Combine(CurrentWaveInformation);
            CurrentWaveInformation?.Destroy();
        }
        
        CurrentWaveInformation = new WaveInformation(this, Wave);        
    }

    private void StartNewWave()
    {
        Wave++;
        SetNewWaveInformation();

        CurrentWaveInformation.ElapsedTime = 0f;
        WaveRemainingEnemyCountToSpawn = Wave * (1+((int)Math.Log(_playerCharacter.currentLevel+1, 2))) * 5; //Find a better function for enemy counts

        WaveEnemySpawnTime = enemySpawnDistributionTime / WaveRemainingEnemyCountToSpawn;

        Debug.Log($"Starting Wave : {Wave} | Enemy Count : {WaveRemainingEnemyCountToSpawn} | Spawn Rate : Every {WaveEnemySpawnTime} seconds");

        if(Wave % BossSpawnWaveInterval == 0) //Every 5 waves, spawn a boss
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        var index = Random.Range(0, _enemySpawnPoints.Count);
        var spawnPoint = _enemySpawnPoints[index];

        var boss = spawnPoint.SpawnBoss(_playerCharacter.transform);
        if(boss != null)
        {
            boss.onEnemyKilled = OnEnemyDied;
            CurrentWaveBosses.Add(boss);
        }
    }

    private void TrySpawningEnemy()
    {
        WaveEnemySpawnTimeRemaining -= Time.deltaTime;
        if (WaveEnemySpawnTimeRemaining <= 0 && WaveRemainingEnemyCountToSpawn > 0 && _enemySpawnPoints.Count > 0)
        {
            WaveEnemySpawnTimeRemaining = WaveEnemySpawnTime;
            var index = Random.Range(0, _enemySpawnPoints.Count);
            var spawnPoint = _enemySpawnPoints[index];
            var spawnedEnemy = spawnPoint.SpawnBasicEnemy(_playerCharacter.transform);
            if(spawnedEnemy != null)
            {
                Enemy enemyComponent = spawnedEnemy.GetComponent<Enemy>();

                enemyComponent.onEnemyKilled = OnEnemyDied;

                CurrentWaveEnemies.Add(spawnedEnemy);
                WaveRemainingEnemyCountToSpawn--;
                Debug.Log($"SpawningEnemy, Will Spawn {WaveRemainingEnemyCountToSpawn} more");
            }
        }
    }

    public void OnEnemyDied(Enemy enemy)
    {
        if(enemy is Boss boss)
        {
            CurrentWaveBosses.Remove(boss);
        }
        else
        {
            CurrentWaveEnemies.Remove(enemy);
        }

        OnEnemyKilled?.Invoke(enemy);
    }

    public static Enemy SpawnEnemy(Vector3 spawnPoint, GameObject typeToSpawn, Transform target)
    {
        if (typeToSpawn == null)
        {
            return null;
        }

        var go = GameObject.Instantiate(typeToSpawn);

        go.transform.position = spawnPoint;

        var enemyComponent = go.GetComponent<Enemy>();

        var movementComponent = go.GetComponent<NavMeshMovement>();
        if (movementComponent != null)
        {
            movementComponent.SetTarget(target);
        }
        return enemyComponent;
    }
}
