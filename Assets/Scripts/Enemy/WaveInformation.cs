using System;
using System.Collections.Generic;
using System.Text;

public class WaveInformation
{

    private EnemySpawner _spawner;
    public Dictionary<string, int> TotalKilledEnemies = new Dictionary<string, int>();
    public float ElapsedTime = 0f;
    public int XPGain = 0;
    public int LevelUpCount = 0;
    public int Wave = 0;

    public WaveInformation(EnemySpawner spawner, int wave)
    {
        _spawner = spawner;
        Wave = wave;
        ExperienceManager.Instance.OnExperienceChange += ExperienceGained;
        Player.OnLevelUp += OnLevelUp;
        _spawner.OnEnemyKilled += OnEnemyKilled;
    }

    private void ExperienceGained(int amount)
    {
        XPGain += 0;
    }

    public void OnLevelUp()
    {
        LevelUpCount++;
    }

    public void Destroy()
    {
        ExperienceManager.Instance.OnExperienceChange -= ExperienceGained;
        Player.OnLevelUp -= OnLevelUp;
        _spawner.OnEnemyKilled -= OnEnemyKilled;
    }

    public void OnEnemyKilled(Enemy enemy)
    {
        if (!TotalKilledEnemies.ContainsKey(enemy.EnemyName))
        {
            TotalKilledEnemies[enemy.EnemyName] = 1;
        }
        else
        {
            TotalKilledEnemies[enemy.EnemyName]++;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Wave {Wave} Ended: ");
        sb.AppendLine($"\tElapsed Time : {ElapsedTime}");
        sb.AppendLine($"\tLevel Ups : {LevelUpCount}");
        sb.AppendLine($"\t\tXP Gained : {XPGain}");
        sb.AppendLine("\tEnemy Kill Counts: ");
        foreach(var kvp in TotalKilledEnemies)
        {
            sb.AppendLine($"\t\t{kvp.Key} : {kvp.Value}");
        }

        return sb.ToString();
    }

    public void Combine(WaveInformation info)
    {
        Wave = info.Wave;
        ElapsedTime += info.ElapsedTime;
        LevelUpCount += info.LevelUpCount;
        XPGain += info.XPGain;
        
        foreach(var kvp in info.TotalKilledEnemies)
        {
            if (!TotalKilledEnemies.ContainsKey(kvp.Key))
            {
                TotalKilledEnemies[kvp.Key] = kvp.Value;
            }
            else
            {
                TotalKilledEnemies[kvp.Key] += kvp.Value;
            }
        }
    }
}
