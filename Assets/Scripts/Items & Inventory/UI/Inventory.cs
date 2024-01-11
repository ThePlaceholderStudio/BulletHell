using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] List<Item> weapons;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;
    private Character character;

    public event Action<Item> OnItemEquipEvent;

    private void Awake()
    {
        character = GameManager.Instance.player.GetComponent<Character>();
    }

    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnButtonClickEvent += OnItemEquipEvent;
        }
    }

    private void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

        AssignItemToSlots();
    }

    private void Update()
    {
        if (character.currentLevel % 5 == 0 && character.currentLevel <= 25)
        {
            AssignWeaponsToSlots();
        }
        else
        {
            AssignItemToSlots();
        }
    }

    private void AssignWeaponsToSlots()
    {
        int i = 0;
        for (; i < weapons.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = weapons[i];
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    private void AssignItemToSlots()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = items[i];
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            AssignItemToSlots();
            return true;
        } 
        else if(weapons.Remove(item))
        {
            AssignWeaponsToSlots();
            return true;
        }
        return false;
    }

    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }
}
