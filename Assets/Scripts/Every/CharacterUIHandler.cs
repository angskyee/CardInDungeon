using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterUIHandler : MonoBehaviour
{
    private HealthSystem _health;
    private CharacterStatsHandler _stats;

    public TextMeshPro Hp;
    public TextMeshPro Def;
    public TextMeshPro Spd;
    public TextMeshPro Atk;
    private void Awake()
    {
        _health = GetComponent<HealthSystem>();
        _stats = GetComponent<CharacterStatsHandler>();
    }

    private void FixedUpdate()
    {
        Hp.text = _health.CurrentHealth.ToString();
        Def.text = _health.CurrentArmor.ToString();
        Spd.text = _stats.CurrentStats.attackSO.speed.ToString();
        Atk.text = _stats.CurrentStats.attackSO.power.ToString();
    }
}
