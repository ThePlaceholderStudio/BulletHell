using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] List<Item> equipment;
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    private void OnValidate()
    {
        if (equipmentSlotsParent != null)
            equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();

        RefreshUI();
    }

    private void RefreshUI()
    {
        int i = 0;
        for (; i < equipment.Count && i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].Item = equipment[i];
        }

        for (; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].Item = null;
        }
    }

    public bool AddItem(Item item)
    {
        if (IsFull())
            return false;

        equipment.Add(item);
        RefreshUI();
        return true;
    }

    public bool IsFull()
    {
        return equipment.Count >= equipmentSlots.Length;
    }
}

