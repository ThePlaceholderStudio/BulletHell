using System.Collections.Generic;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{
    [SerializeField] List<Item> weapons;
    [SerializeField] Transform weaponSlotsParent;
    [SerializeField] WeaponSlot[] weaponSlots;

    private void OnValidate()
    {
        if (weaponSlotsParent != null)
            weaponSlots = weaponSlotsParent.GetComponentsInChildren<WeaponSlot>();

        RefreshUI();
    }

    private void RefreshUI()
    {
        int i = 0;
        for (; i < weapons.Count && i < weaponSlots.Length; i++)
        {
            weaponSlots[i].Item = weapons[i];
        }

        for (; i < weaponSlots.Length; i++)
        {
            weaponSlots[i].Item = null;
        }
    }

    public bool AddItem(Item weapon)
    {
        if (IsFull())
            return false;

        weapons.Add(weapon);
        RefreshUI();
        return true;
    }

    public bool IsFull()
    {
        return weapons.Count >= weaponSlots.Length;
    }
}