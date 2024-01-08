using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;

    private void Awake()
    {
        GameManager.Instance.player = PlayerSpawn();
        ActivateGameObjectAndComponents();
    }

    private GameObject PlayerSpawn()
    {
        var player = Instantiate(SelectionManager.Instance.PlayerPrefab, GameManager.Instance.SpawnPoint.transform.position, Quaternion.identity);

        //var player = Instantiate(playerPrefab, GameManager.Instance.SpawnPoint.transform.position, Quaternion.identity);

        player.transform.parent = GameManager.Instance.SpawnPoint.transform;

        return player;
    }

    void ActivateGameObjectAndComponents()
    {
        //// Detach the GameObject from its parent
        //characters[selectionIndex].transform.parent = null;

        // Get all components of type Behaviour
        Behaviour[] components = GameManager.Instance.player.GetComponents<Behaviour>();

        // Loop through all components and enable them
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }
}
