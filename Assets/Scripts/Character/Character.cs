using UnityEngine;

public class Character : MonoBehaviour
{
    public int currentExperience, maxExperience, currentLevel;

    public delegate void LevelUpEvent();
    public static event LevelUpEvent OnLevelUp;

    public delegate void EquipEvent();
    public static event EquipEvent OnEquip;

    private WeaponSystem weaponSystem; 
    private int equippedSlotIndex; 

    public CharacterStat MaxHp;
    public CharacterStat LifeRegen;
    public CharacterStat Armor;
    public CharacterStat DashCoolDown;
    public CharacterStat DashRange;
    public CharacterStat MoveSpeed;
    public CharacterStat Damage;
    public CharacterStat FireRate;
    public CharacterStat ReloadSpeed;
    public CharacterStat CriticalChance;
    public CharacterStat CriticalDamage;
    public CharacterStat PickUpRadius;
    public CharacterStat XPGain;

    //ToDo Weapon Class
    public CharacterStat Range;
    public CharacterStat ReloadTime;
    public CharacterStat MagazineSize;
    public CharacterStat RPM;
    public CharacterStat ProjectileCount;
    public CharacterStat TargetPenetration;
    public CharacterStat ConicalAngle;

    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;
    [SerializeField] WeaponPanel weaponPanel;

    private void Awake()
    {
        statPanel.SetStats(MaxHp, LifeRegen, Armor, DashCoolDown, DashRange, MoveSpeed, Damage, FireRate, ReloadSpeed, CriticalChance, CriticalDamage, PickUpRadius, XPGain);
        statPanel.UpdateStatValues();

        inventory.OnItemEquipEvent += EquipFromInventory;
    }

    private void Start()
    {
        // Initialize the WeaponSystem reference
        weaponSystem = FindObjectOfType<WeaponSystem>();
    }

    private void EquipFromInventory(Item item)
    {
        if (item is EquippableItem)
        {
            Equip((EquippableItem)item);
        }
    }

    public void Equip(EquippableItem item)
    {
        if (inventory.RemoveItem(item) && item.ItemType != ItemType.Weapon)
        {
            if (equipmentPanel.AddItem(item))
            {
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
        }
        else
        {
            weaponPanel.AddItem(item);

            // Store the equipped slot index
            equippedSlotIndex = (int)item.WeaponType;

            // Subscribe to the OnItemEquippedEvent
            item.OnItemEquippedEvent += ActivateWeapon;

            item.Equip(this);
            statPanel.UpdateStatValues();
        }

        Debug.Log("equip");
        OnEquip?.Invoke();
    }

    private void ActivateWeapon(EquippableItem item)
    {
        // Activate the weapon in the WeaponSystem
        weaponSystem.SetActiveWeapon(equippedSlotIndex);

    }

    private void OnTriggerEnter(Collider collider)
    {
        ICollectible collectible = collider.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.Collect();
        }
    }

    private void OnEnable()
    {
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }


    private void HandleExperienceChange(int experience)
    {
        currentExperience += experience;

        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        while (currentExperience >= maxExperience)
        {
            currentLevel++;
            currentExperience -= maxExperience;
            maxExperience += 100;
            OnLevelUp?.Invoke();
        }
    }
}

