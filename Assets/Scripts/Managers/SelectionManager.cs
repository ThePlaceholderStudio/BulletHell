using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }

    public GameObject PlayerPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
