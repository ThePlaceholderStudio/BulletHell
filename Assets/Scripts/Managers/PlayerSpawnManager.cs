using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public bool isEditorModeActive;

    private void Awake()
    {
        GameManager.Instance.player = PlayerSpawn();
        ActivateGameObjectAndComponents();
    }

    private GameObject PlayerSpawn()
    {
        GameObject player;

        if (isEditorModeActive)
        {
            player = Instantiate(playerPrefab, GameManager.Instance.SpawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            player = Instantiate(SelectionManager.Instance.PlayerPrefab, GameManager.Instance.SpawnPoint.transform.position, Quaternion.identity);
        }

        player.transform.parent = GameManager.Instance.SpawnPoint.transform;

        return player;
    }

    void ActivateGameObjectAndComponents()
    {
        Behaviour[] components = GameManager.Instance.player.GetComponents<Behaviour>();

        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }
}
