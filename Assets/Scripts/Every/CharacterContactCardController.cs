using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterContactCardController : MonoBehaviour
{
    protected Action<GameObject, HealthSystem> OnContactEnemyCard;

    [FormerlySerializedAs("ConttactEnemy")] public bool ContactEnemy;
    public bool ContactInventory;
    
    private float attackDeray;
    protected HealthSystem _healthSystem;
    private HealthSystem _collidingTargetHealthSystem;
    protected Transform _direction;
    private GameObject _enemyGameObject;
    protected CharacterStatsHandler Stats { get; private set; }

    protected virtual void Awake()
    {
        Stats = GetComponent<CharacterStatsHandler>();
        _direction = GetComponent<Transform>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        ContactInventory = false;
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
}
