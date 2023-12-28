using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContactCardController : MonoBehaviour
{
    protected Action<GameObject, HealthSystem> OnContactEnemyCard;

    public bool ConttactEnemy;
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

    private void FixedUpdate()
    {
        if (ConttactEnemy == false && attackDeray < Stats.CurrentStats.attackSO.speed + 10.0f)
        {
            attackDeray += Time.fixedDeltaTime;
            if (attackDeray > Stats.CurrentStats.attackSO.speed)
            {
                ConttactEnemy = true;
                attackDeray = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _enemyGameObject = collision.gameObject;

        if (_enemyGameObject.layer == LayerMask.NameToLayer("Enemy") && ConttactEnemy)
        {
            _collidingTargetHealthSystem = _enemyGameObject.GetComponent<HealthSystem>();
            CallOnContactEnemyCard(_enemyGameObject,_collidingTargetHealthSystem);
            ConttactEnemy = false;
        }
    }

    private void CallOnContactEnemyCard(GameObject enemyGameObject, HealthSystem collidingTargetHealthSystem)
    {
        OnContactEnemyCard?.Invoke(enemyGameObject, collidingTargetHealthSystem);
    }
}
