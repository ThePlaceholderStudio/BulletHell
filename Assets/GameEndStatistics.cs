using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndStatistics : MonoBehaviour
{
    [SerializeField]
    private Text Text;

    // Start is called before the first frame update
    void Start()
    {        
    }

    private void Awake()
    {
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {        
    }    

    // Update is called once per frame
    void Update()
    {
        if(EnemySpawner.TotalWaveInformation != null)
        {
            Text.text = EnemySpawner.TotalWaveInformation.ToString();
        }
    }

    private void OnPlayerDied()
    {
        
    }
}