using UnityEngine;

public class WeaponSystem : SystemBase
{
    void Start()
    {
        ItemIndex = StartingItemIndex;
        SetActiveItem(ItemIndex);
    }
}