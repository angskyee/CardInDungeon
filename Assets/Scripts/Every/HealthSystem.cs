using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    private CharacterStatsHandler _statsHandler;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    
    public int CurrentHealth { get; private set; }
    public int CurrentArmor { get; private set; }

    private int MaxHealth => _statsHandler.CurrentStats.maxHealth;
    private int MaxArmor => _statsHandler.CurrentStats.maxArmor;

    private void Awake()
    {
        _statsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        CurrentHealth = _statsHandler.CurrentStats.maxHealth;
        CurrentArmor = _statsHandler.CurrentStats.maxArmor;
    }

    public bool ChangeHealth(int change)
    {
        if (change == 0)
        {
            return false;
        }
        
        CurrentHealth += (change - MaxArmor);
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