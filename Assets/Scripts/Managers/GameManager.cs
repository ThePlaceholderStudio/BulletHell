using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [System.NonSerialized]
    public GameObject player;

    public Transform PlayerCam;
    public Transform SpawnPoint;

    public Inventory inventory;
    public EquipmentPanel equipmentPanel;
    public StatPanel statPanel;
    public WeaponPanel weaponPanel;

    private void Awake()
    {
        Debug.Log("awake");
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
