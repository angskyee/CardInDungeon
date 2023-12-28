using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipItem : MonoBehaviour
{
    private PlayerMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _movement.OnEquipItemEvent += Equipable;
    }

    private void Equipable(int value)
    {
        throw new NotImplementedException();
    }
}
