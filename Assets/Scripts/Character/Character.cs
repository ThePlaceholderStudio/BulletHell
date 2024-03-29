using UnityEngine;

public class Character : MonoBehaviour
{
    public int currentExperience, maxExperience, currentLevel;

    public delegate void LevelUpEvent();
    public static event LevelUpEvent OnLevelUp;

    public delegate void EquipEvent();
    public static event EquipEvent OnEquip;

    public ActivationSystem WeaponSystem;
    public ActivationSystem UtilitySystem;
    private int equippedSlotIndex;

    public EquippableItem DefaultWeapon;

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

    private Inventory inventory;
    private EquipmentPanel equipmentPanel;
    private StatPanel statPanel;
    private WeaponPanel weaponPanel;


    private void Start()
    {
        inventory = GameManager.Instance.inventory;
        equipmentPanel = GameManager.Instance.equipmentPanel;
        statPanel = GameManager.Instance.statPanel;
        weaponPanel = GameManager.Instance.weaponPanel;

        statPanel.SetStats(MaxHp, LifeRegen, Armor, DashCoolDown, DashRange, MoveSpeed, Damage, FireRate, ReloadSpeed, CriticalChance, CriticalDamage, PickUpRadius, XPGain);
        statPanel.UpdateStatValues();

        inventory.OnItemEquipEvent += EquipFromInventory;

        WeaponSystem = WeaponSystem.GetComponent<ActivationSystem>();
        UtilitySystem = UtilitySystem.GetComponent<ActivationSystem>();

        Equip(DefaultWeapon);
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
        if (inventory.RemoveItem(item))
        {
            switch (item.ItemType)
            {
                case ItemType.Weapon:
                    weaponPanel.AddItem(item);
                    equippedSlotIndex = (int)item.WeaponType;
                    item.OnItemEquippedEvent += ActivateWeapon;
                    item.Equip(this);
                    statPanel.UpdateStatValues();
                    break;
                case ItemType.Utility:
                    if (equipmentPanel.AddItem(item))
                    {
                        equippedSlotIndex = (int)item.Utility;
                        item.OnItemEquippedEvent += ActivateUtility;
                        item.Equip(this);
                        statPanel.UpdateStatValues();
                    }
                    break;
                default:
                    if (equipmentPanel.AddItem(item))
                    {
                        item.Equip(this);
                        statPanel.UpdateStatValues();
                    }
                    break;
            }
            Debug.Log("equip");
            OnEquip?.Invoke();
        }
    }

    private void ActivateWeapon(EquippableItem item)
    {
        if (WeaponSystem != null)
        {
            WeaponSystem.SetActiveItem(equippedSlotIndex);
        }
        else
        {
            Debug.Log("WeaponSystem is null");
        }
    }

    private void ActivateUtility(EquippableItem item)
    {
        if (UtilitySystem != null)
        {
            UtilitySystem.SetActiveItem(equippedSlotIndex);
        }
        else
        {
            Debug.Log("UtilitySystem is null");
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