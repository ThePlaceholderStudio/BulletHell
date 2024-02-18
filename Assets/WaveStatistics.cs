using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class WaveStatistics : MonoBehaviour
{
    public Text Text;

    private EnemySpawner _spawner;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var go in gameObject.scene.GetRootGameObjects())
        {
            if (go.GetComponent<EnemySpawner>() != null)
            {
                var enemySpawner = go.GetComponentInChildren<EnemySpawner>();
                _spawner = enemySpawner;
                _spawner.OnWaveEnded += OnWaveEnd;
            }
        }
    }

    private void OnWaveEnd(WaveInformation info)
    {
        Text.text = info.ToString(); 

        Text.enabled = true;

        StartCoroutine(WaveEndTick());
    }

    private IEnumerator WaveEndTick()
    {
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
