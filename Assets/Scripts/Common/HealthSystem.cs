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
    public int CurrentMaxArmor { get; private set; }

    public int MaxHealth => _statsHandler.CurrentStats.maxHealth;
    public int MaxArmor => _statsHandler.CurrentStats.maxArmor;

    private void Awake()
    {
        _statsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        CurrentHealth = _statsHandler.CurrentStats.maxHealth;
        CurrentMaxArmor = _statsHandler.CurrentStats.maxArmor;
    }

    public bool ChangeHealth(int change)
    {
        Debug.Log(change);
        Debug.Log(CurrentHealth);
        
        if (change == 0)
        {
            return false;
        }
        
        CurrentHealth += (change - MaxArmor);
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;
        
        Debug.Log("changedHealth"+CurrentHealth);
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