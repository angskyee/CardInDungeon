using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    public event Action<bool> OnClickEvent;
    
    private Camera _camera;
    private PlayerContactCardController _contact;

    private void Awake()
    {
        _camera = Camera.main;
        _contact = GetComponent<PlayerContactCardController>();
    }

    public void OnLook(InputValue value)
    {
        Vector2 mouseAim = _camera.ScreenToWorldPoint(value.Get<Vector2>());
        if(_contact.ContactInventory != true)
        CallSelectCardEvent(mouseAim);
        else
        CallInventoryLook(mouseAim);
    }

    public void OnClick(InputValue value)
    {
        IsClicking = value.isPressed;
        OnClickEvent?.Invoke(IsClicking);
    }
}
