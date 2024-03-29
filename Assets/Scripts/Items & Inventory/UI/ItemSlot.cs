﻿using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image image;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI statsText;
    [SerializeField] TextMeshProUGUI itemDescriptionText;
    [SerializeField] TextMeshProUGUI itemRarityText;

    public event Action<Item> OnButtonClickEvent;

    private StringBuilder sb = new StringBuilder();

    private Item _item;
    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;

            if (_item == null)
            {
                image.enabled = false;
            }
            if (_item != null && image != null)
            {
                image.sprite = _item.Icon;
                image.enabled = true;

                if (!(_item is EquippableItem))
                {
                    return;
                }

                EquippableItem item = (EquippableItem)_item;

                gameObject.SetActive(true);

                if (_item != null && nameText != null)
                {
                    nameText.text = _item.ItemName;
                }

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

                if (_item != null && statsText != null)
                    statsText.text = sb.ToString();

                if (_item != null && itemRarityText != null)
                    itemRarityText.text = item.EquipmentRarity.ToString();

                if (_item != null && itemDescriptionText != null)
                    itemDescriptionText.text = item.ItemDescription;
            }
        }
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

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();

        if (nameText == null)
            nameText = GetComponent<TextMeshProUGUI>();

        if (statsText == null)
            statsText = GetComponent<TextMeshProUGUI>();

        if (itemDescriptionText == null)
            itemDescriptionText = GetComponent<TextMeshProUGUI>();
    }

    public void OnButtonClick()
    {
        if (Item != null && OnButtonClickEvent != null)
            OnButtonClickEvent(Item);
        PanelManager.Instance.ToggleTraitsPanel(); 
    }
}
