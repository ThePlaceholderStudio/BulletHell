using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveProgress : MonoBehaviour
{

    public EnemySpawner EnemySpawner;
    public Text Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemySpawner != null && Text != null)
        {
            Text.text = $"Wave : {EnemySpawner.Wave}" +
                $"\n" +
                $"RemainingEnemyCount : {EnemySpawner.WaveRemainingEnemyCountToSpawn}" +
                $"\n" +
                $"Elapsed Time This Wave : {EnemySpawner.ElapsedTimeThisWave}";
        }
    }    
}
