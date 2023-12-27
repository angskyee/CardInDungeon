using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    private bool healthChange;
    private CharacterStatsHandler _statsHandler;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public int CurrentHealth { get; private set; }
    public int CurrentMaxArmor { get; private set; }
    public int CurrentDistance { get; private set; }
    public bool CurrentTurn { get; private set; }

    public int MaxHealth => _statsHandler.CurrentStats.maxHealth;
    public int MaxArmor => _statsHandler.CurrentStats.maxArmor;
    public int Distance => _statsHandler.CurrentStats.distance;
    public bool Turn => _statsHandler.CurrentStats.turn;

    private void Awake()
    {
        _statsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        CurrentHealth = _statsHandler.CurrentStats.maxHealth;
        CurrentMaxArmor = _statsHandler.CurrentStats.maxArmor;
        CurrentDistance = _statsHandler.CurrentStats.distance;
        CurrentTurn = _statsHandler.CurrentStats.turn;
    }

    private void Update()
    {
        if(healthChange == true)
            OnInvincibilityEnd?.Invoke();
    }

    public bool ChangeHealth(int change)
    {
        if (change == 0 || healthChange != true)
        {
            return false;
        }

        healthChange = false;
        
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (change > 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
        }

        if (CurrentHealth <= 0f)
        {
            CallDeath();
        }

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}