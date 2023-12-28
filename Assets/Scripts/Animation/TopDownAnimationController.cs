using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAnimationController : TopDownAnimations
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");
    

    protected override void Awake()
    {
        base.Awake();

    }

    // Start is called before the first frame update
    void Start()
    {
        controller.OnSelectPlayerCharacterCardEvent += Move;
        controller.OnMoved += OnMoved;
        
        if(healthSystem != null)
        {
            healthSystem.OnDamage += Hit;
            healthSystem.OnInvincibilityEnd += InvincibilityEnd;
        }
    }

    private void Move(GameObject gameObject, Vector2 direction)
    {
        animator.SetBool(IsWalking, true);
    }
    
    private void OnMoved()
    {
        animator.SetBool(IsWalking, false);
    }
    private void Hit()
    {
        animator.SetBool(IsHit,true);
    }

    private void InvincibilityEnd()
    {
        animator.SetBool(IsHit, false);
    }
}