using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAnimations : MonoBehaviour
{
    protected Animator animator;
    protected PlayerController controller;
    protected HealthSystem healthSystem;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<PlayerController>();
        healthSystem = GetComponent<HealthSystem>();
    }
}