using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContactEnemy : CharacterContactCardController
{
    private CharacterStatsHandler EnemyStats;
    private Rigidbody2D _rigidbody;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
        OnContactEnemyCard += OnDamage;
    }

    private void OnDamage(GameObject enemyGameObject, HealthSystem enemyHealthSystem)
    {
        EnemyStats = enemyGameObject.GetComponent<CharacterStatsHandler>();

        _healthSystem.ChangeHealth(-EnemyStats.CurrentStats.attackSO.power);
        enemyHealthSystem.ChangeHealth(-Stats.CurrentStats.attackSO.power);
        
        OnKnockBack(enemyGameObject, EnemyStats);
    }

    private void OnKnockBack(GameObject enemyGameObject, CharacterStatsHandler enemyStats)
    {
        Vector2 knockBackDirection =
            -(enemyGameObject.transform.position - _direction.position).normalized *
            enemyStats.CurrentStats.attackSO.knockbackPower;

        _rigidbody.AddForce(knockBackDirection, ForceMode2D.Impulse);
    }
}
