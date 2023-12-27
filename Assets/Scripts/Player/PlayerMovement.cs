using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _controller;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _controller.OnSelectCardEvent += Move;
    }

    private void Move(GameObject obj, Vector2 direction)
    {
        obj.transform.position = direction;
    }
}
