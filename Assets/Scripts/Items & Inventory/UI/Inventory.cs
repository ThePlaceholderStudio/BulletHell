using System;
using System.Collections.Generic;
using System.Linq;
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
    }


    private void OnEnable()
    {
        AssignItemToSlots();
    }

    private void OnDisable()
    {
        Shuffle(items);
    }

    private void Update()
    {
        if (character.currentLevel % 5 == 0 && character.currentLevel < 25)
        {
            AssignWeaponsToSlots();
        }
    }

    public List<Item> GetRandomItems(List<Item> items)
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        Dictionary<Rarity, int> weights = new Dictionary<Rarity, int>()
    {
        { Rarity.Common, 50 },
        { Rarity.Uncommon, 30 },
        { Rarity.Rare, 15 },
        { Rarity.Epic, 4 },
        { Rarity.Legendary, 1 }
    };
        int totalWeight = weights.Values.Sum();

        List<Item> unpickedItems = new List<Item>(items);
        List<Item> pickedItems = new List<Item>();

        for (int i = 0; i < itemSlots.Length; i++)
        {
            int randomNumber = Random.Range(0, totalWeight);

            int weightSum = 0;
            foreach (EquippableItem item in items)
            {
                weightSum += weights[item.EquipmentRarity];

                if (weightSum > randomNumber && pickedItems.All(pickedItem => pickedItem.ItemName != item.ItemName))
                {
                    unpickedItems.Remove(item);
                    pickedItems.Add(item);
                    break;
                }
            }
        }

        return pickedItems;
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
        List<Item> pickedItems = GetRandomItems(items).ToList();
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = pickedItems[i];
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
        else if (weapons.Remove(item))
        {
            AssignWeaponsToSlots();
            return true;
        }
        return false;
    }

    public static void Shuffle<T>(List<T> list)
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
