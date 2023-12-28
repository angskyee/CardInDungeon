using System;
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
    private PlayerMovement _movement;
    
    public bool equipped;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _movement.OnEquipItemEvent += PlayerContactItem;
    }

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

    public void PlayerContactItem(int value)
    {
        Inventory.instance.SelectItem(value);
    }
}