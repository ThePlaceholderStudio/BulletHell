using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ItemTooltip : MonoBehaviour
{
    public static ItemTooltip Instance;

    [SerializeField] Text nameText;
    [SerializeField] Text slotTypeText;
    [SerializeField] Text statsText;
    [SerializeField] Text itemDescriptionText;

    private StringBuilder sb = new StringBuilder();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        gameObject.SetActive(false);
    }

    public void ShowTooltip(Item itemToShow)
    {
        if (!(itemToShow is EquippableItem))
        {
            return;
        }

        EquippableItem item = (EquippableItem)itemToShow;

        gameObject.SetActive(true);

        nameText.text = item.ItemName;

        sb.Length = 0;

        AddStatText(item.MaxHpBonus, " MaxHp");
        AddStatText(item.LifeRegenBonus, " Life Regen");
        AddStatText(item.ArmorBonus, " Armor");
        AddStatText(item.DashCoolDownBonus, " Dash Cool-Down");
        AddStatText(item.DashRangeBonus, " Dash Range");
        AddStatText(item.MoveSpeedBonus, " Move Speed");
        AddStatText(item.DamageBonus, " Damage");
        AddStatText(item.FireRateBonus, " Fire Rate");
        AddStatText(item.ReloadSpeedBonus, " Reload Speed");
        AddStatText(item.CriticalChanceBonus, " Critical Chance");
        AddStatText(item.CriticalDamageBonus, " Critical Damage");
        AddStatText(item.PickUpRadiusBonus, " Pick Up Radius");
        AddStatText(item.XPGainBonus, " XP Gain");

        AddStatText(item.MaxHpPercentBonus * 100, "% MaxHp");
        AddStatText(item.LifeRegenPercentBonus * 100, "% Life Regen");
        AddStatText(item.ArmorPercentBonus * 100, "% Armor");
        AddStatText(item.DashCooldownPercentBonus * 100, "% Dash Cool-Down");
        AddStatText(item.DashRangePercentBonus * 100, "% Dash Range");
        AddStatText(item.MoveSpeedPercentBonus * 100, "% Move Speed");
        AddStatText(item.DamagePercentBonus * 100, "% Damage");
        AddStatText(item.FireRatePercentBonus * 100, "% Fire Rate");
        AddStatText(item.ReloadSpeedPercentBonus * 100, "% Reload Speed");
        AddStatText(item.CriticalChancePercentBonus * 100, "% Critical Chance");
        AddStatText(item.CriticalDamagePercentBonus * 100, "% Critical Damage");
        AddStatText(item.PickUpRadiusPercentBonus * 100, "% Pick Up Radius");
        AddStatText(item.XPGainPercentBonus * 100, "% XP Gain");

        statsText.text = sb.ToString();

        itemDescriptionText.text = item.ItemDescription;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void AddStatText(float statBonus, string statName)
    {
        if (statBonus != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (statBonus > 0)
                sb.Append("+");

            sb.Append(statBonus);
            sb.Append(statName);
        }
    }
}
