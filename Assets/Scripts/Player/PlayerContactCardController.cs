using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerContactCardController : MonoBehaviour
{
    public Action<GameObject, HealthSystem> OnContactEnemyCard;

    [FormerlySerializedAs("ConttactEnemy")] public bool ContactEnemy;
    public bool ContactInventory;
    private bool IsClick;
    private float attackDeray;
    
    protected Transform _direction;
    private GameObject _enemyGameObject;
    
    private HealthSystem _collidingTargetHealthSystem;
    private PlayerMovement _movement;
    private PlayerController _controller;
    protected HealthSystem _healthSystem;
    protected CharacterStatsHandler Stats { get; private set; }

    protected virtual void Awake()
    {
        IsClick = false;
        Stats = GetComponent<CharacterStatsHandler>();
        _controller = GetComponent<PlayerController>();
        _direction = GetComponent<Transform>();
        _healthSystem = GetComponent<HealthSystem>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        ContactInventory = false;
        _movement.OnEquipItemEvent += PlayerContactItem;
    }

    private void FixedUpdate()
    {
        if (ContactEnemy == false && attackDeray < Stats.CurrentStats.attackSO.speed + 10.0f)
        {
            attackDeray += Time.fixedDeltaTime;
            if (attackDeray > Stats.CurrentStats.attackSO.speed)
            {
                ContactEnemy = true;
                attackDeray = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _enemyGameObject = collision.gameObject;

        if (_enemyGameObject.layer == LayerMask.NameToLayer("Enemy") && ContactEnemy)
        {
            _collidingTargetHealthSystem = _enemyGameObject.GetComponent<HealthSystem>();
            CallOnContactEnemyCard(_enemyGameObject,_collidingTargetHealthSystem);
            ContactEnemy = false;
        }
        else if (_enemyGameObject.layer == LayerMask.NameToLayer("Inventory"))
        {
            ContactInventory = true;
            Debug.Log("IsInventory");
        }
    }

    private void CallOnContactEnemyCard(GameObject enemyGameObject, HealthSystem collidingTargetHealthSystem)
    {
        OnContactEnemyCard?.Invoke(enemyGameObject, collidingTargetHealthSystem);
    }
    
    public void PlayerContactItem(int value)
    {
        Debug.Log(value);
        if(value > 9 && IsClick)
            Inventory.instance.SelectItem(value - 10);
    }
}
