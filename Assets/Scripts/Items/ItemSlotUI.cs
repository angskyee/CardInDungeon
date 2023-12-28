using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public SpriteRenderer icon;
    public TextMeshPro quatityText;
    private ItemSlot curSlot;

    public int index;
    public bool equipped;
    public void Set(ItemSlot slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
        quatityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;
    }

    public void Clear()
    {
        curSlot = null;
        quatityText.text = string.Empty;
        icon.gameObject.SetActive(false);
    }

    public void PlayerContactItem()
    {
        Inventory.instance.SelectItem(index);
    }
}