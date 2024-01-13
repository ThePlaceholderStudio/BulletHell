using System.Collections.Generic;
using System.Text;

public class WaveInformation
{
    public Dictionary<string, int> TotalKilledEnemies = new Dictionary<string, int>();
    public float ElapsedTime = 0f;
    public int XPGain = 0;
    public int LevelUpCount = 0;

    public void OnLevelUp()
    {
        LevelUpCount++;
    }

    public void OnEnemyKilled(Enemy enemy)
    {
        TotalKilledEnemies[enemy.EnemyName] = 0;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Elapsed Time : {ElapsedTime}");
        sb.AppendLine($"Level Ups : {LevelUpCount}");
        sb.AppendLine($"\tXP Gained : {XPGain}");
        sb.AppendLine("Enemy Kill Counts: ");
        foreach(var kvp in TotalKilledEnemies)
        {
            sb.AppendLine($"\t{kvp.Key} : {kvp.Value}");
        }

        return sb.ToString();
    }
}
