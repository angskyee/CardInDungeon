using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats baseStats;
    public CharacterStats CurrentStats { get; private set; }
    public List<CharacterStats> statsModifiers  = new List<CharacterStats>();

    private void Awake()
    {
        UpdateCharacterStats();    
    }

    private void UpdateCharacterStats()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }

        CurrentStats = new CharacterStats { attackSO = attackSO };
        UpdateStats((a, b) => b, baseStats);
        if (CurrentStats.attackSO != null)
        {
            CurrentStats.attackSO.target = baseStats.attackSO.target;
        }

        foreach (CharacterStats modifier in statsModifiers.OrderBy(o => o.statsChangeType))
        {
            if (modifier.statsChangeType == StatsChangeType.Override)
            {
                UpdateStats((o, o1) => o1, modifier);
            }
            else if (modifier.statsChangeType == StatsChangeType.Add)
            {
                UpdateStats((o, o1) => o + o1, modifier);
            }
            else if (modifier.statsChangeType == StatsChangeType.Multiple)
            {
                UpdateStats((o, o1) => o * o1, modifier);
            }
        }

    }
    private void UpdateStats(Func<int, int, int> operation, CharacterStats newModifier)
    {
        CurrentStats.maxHealth = operation(CurrentStats.maxHealth, newModifier.maxHealth);
        CurrentStats.maxArmor = operation(CurrentStats.maxArmor, newModifier.maxArmor);
        if (CurrentStats.attackSO== null || newModifier.attackSO == null)
            return;

        UpdateAttackStats(operation, CurrentStats.attackSO, newModifier.attackSO);
    }
    
    private void UpdateAttackStats(Func<int, int, int> operation, AttackSO currentAttack, AttackSO newAttack)
    {
        if (currentAttack == null || newAttack == null)
        {
            return;
        }
        
        currentAttack.power = operation(currentAttack.power, newAttack.power);
        currentAttack.distance = operation(currentAttack.distance, newAttack.distance);
        currentAttack.speed = (int)operation((int)currentAttack.speed, (int)newAttack.speed);
    }
}