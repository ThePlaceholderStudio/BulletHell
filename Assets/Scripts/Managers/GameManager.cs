using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [System.NonSerialized]
    public GameObject player;

    public ExperienceManager experienceManager;

    public Transform PlayerCam;
    public Transform SpawnPoint;

    public Inventory inventory;
    public EquipmentPanel equipmentPanel;
    public StatPanel statPanel;
    public WeaponPanel weaponPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        experienceManager = Instantiate(experienceManager);
    }
}
