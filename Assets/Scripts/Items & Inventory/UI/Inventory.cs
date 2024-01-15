using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public Item GetRandomItem(List<Item> items)
    {
        // Assign a rarity to each item.
        foreach (EquippableItem item in items)
        {
            item.EquipmentRarity = (Rarity)Random.Range(0, 5);
        }

        // Assign a weight to each rarity.
        int[] weights = new int[5] { 50, 30, 15, 4, 1 };

        // Calculate the total weight of all the rarities.
        int totalWeight = 0;
        foreach (int weight in weights)
        {
            totalWeight += weight;
        }

        // Generate a random number between 0 and the total weight.
        int randomNumber = Random.Range(0, totalWeight);

        // Iterate through the list of items, adding up the weights of the rarities as you go.
        int weightSum = 0;
        foreach (EquippableItem item in items)
        {
            weightSum += weights[(int)item.EquipmentRarity];

            // When the sum of the weights exceeds the random number, select the current item and return it.
            if (weightSum > randomNumber)
            {
                return item;
            }
        }

        // If the end of the list is reached without selecting an item, return the last item.
        return items[items.Count - 1];
    }

    private void OnEnable()
    {
        Debug.Log("inv enb");

        AssignItemToSlots();
    }

    private void Update()
    {
        if (character.currentLevel % 5 == 0 && character.currentLevel <= 25)
        {
            AssignWeaponsToSlots();
        }
        //else
        //{
        //    AssignItemToSlots();
        //}
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
            itemSlots[i].Item = GetRandomItem(items);
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
