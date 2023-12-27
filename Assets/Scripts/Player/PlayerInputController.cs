using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnLook(InputValue value)
    {
        Vector2 mouseAim = _camera.ScreenToWorldPoint(value.Get<Vector2>());
        CallSelectCardEvent(mouseAim);
    }

    public void OnClick(InputValue value)
    {
        IsClicking = value.isPressed;
    }
}
