using UnityEngine.EventSystems;

public class EquipmentSlot : ItemSlot, IPointerEnterHandler, IPointerExitHandler
{
    public Rarity EquipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = EquipmentType.ToString() + " Slot";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemTooltip.Instance.ShowTooltip(Item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemTooltip.Instance.HideTooltip();
    }
}

