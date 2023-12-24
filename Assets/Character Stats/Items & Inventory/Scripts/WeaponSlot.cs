using UnityEngine.EventSystems;

public class WeaponSlot : ItemSlot
{
    public ItemType ItemType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = ItemType.ToString() + " Slot";
    }
}

