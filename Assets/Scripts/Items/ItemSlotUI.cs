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
    public int index;
    private ItemSlot curSlot;
    private PlayerMovement _movement;
    private PlayerInputController _controller;
    private bool isClick;
    
    public bool equipped;

    private void Awake()
    {
        isClick = false;
        _movement = GetComponent<PlayerMovement>();
        _controller = GetComponent<PlayerInputController>();
    }

    private void Start()
    {
        _movement.OnEquipItemEvent += PlayerContactItem;
        _controller.OnClickEvent += Click;
    }

    private void Click(bool value)
    {
        isClick = value;
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
        if(value > 9 && isClick)
        Inventory.instance.SelectItem(value - 10);
    }
}