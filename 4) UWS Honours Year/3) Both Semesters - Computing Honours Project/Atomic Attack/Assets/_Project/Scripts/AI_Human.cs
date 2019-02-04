﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_Human : MonoBehaviour {

    // Parent Class for Inheritance.
    Rigidbody2D Rigidbody2D;
    Animator Animator;
    protected HealthSystem HealthSystem;

    [Space(-10), Header("[ Parent: AI_Human ] Movement")]
    public int CostValue;
    public int ScoreValue;

    [Space( 10), Header("[ Parent: AI_Human ] Movement")]
    [SerializeField] protected float MovementSpeed;
    protected float MovementSpeedInitial;
    protected int MovementDirection;
    public bool Grounded;
    public bool RunAway;    // For 16_Sulphur as a deterent, so can't get close. If doesn't work, switch to Poison.
    public bool Suffocate;  // For 07_Nitrogen + 15_Phosphorus. (DoT)
    public bool Poisoned;   // For 09_Flourine + 17_Chlorine.   (DoT)
    public bool Burned;     // For 18_Argon, flamethrower guy.  (DoT)
    [SerializeField] protected GameObject EffectSuffocate;
    [SerializeField] protected GameObject EffectPoisoned;
    [SerializeField] protected GameObject EffectBurned;

    [Space( 10), Header("---- Target ----")]
    public Transform Target;
    [SerializeField] protected float TargetHealth;
    public Transform FinalTarget;

    [Space( 10), Header("----------------- Stats -----------------")]
    [SerializeField] protected float LookRadius;
    [SerializeField] protected float AttackRadius;
    [SerializeField] protected float AttackRate;
    protected float NextAttackTime = 0;

    protected virtual void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        HealthSystem = GetComponent<HealthSystem>();
        MovementSpeedInitial = MovementSpeed;
    }

    protected virtual void Movement()
    {   // When velocity = 0, can't start moving again. fix???
        try
        {
            Rigidbody2D.velocity = new Vector2(MovementSpeed * -MovementDirection, 0);
            transform.localScale = new Vector2(0.3f * MovementDirection, 0.3f);

            // Calculate the distance inbetween Target and Self. Will stop if inside AttackRadius.
            float AttackRange = Vector2.Distance(transform.position, Target.transform.position);
            if (AttackRange <= AttackRadius)
            {   // If inside AttackRadius, will start damaging enemy!
                MovementSpeed = 0;
                PlayAnimationAttack();
            }
            else
            {
                MovementSpeed = MovementSpeedInitial;
                Animator.Play("Run");
            }
        }
        catch (System.NullReferenceException) { };
    }

    protected virtual void PlayAnimationAttack()
    {
        if (Time.time > NextAttackTime)
        {    // Animator.Play(state, layer, normalizedTime);
             // Need the other 2 overloads, otherwise won't repeat every AttackRate.
            Animator.Play("Attack", -1, 0);
            NextAttackTime = Time.time + AttackRate;
            // If Swordsman, Playing Animation will make DamageArea active, triggering Damage to be taken.
            Shoot();
        }
    }

    protected virtual void PlayAnimationDeath()
    {
        Animator.Play("Die");
        Destroy(gameObject, 1f);
        return;
        // Add to score or collateral damage score
    }

    // Added two new Layers - "Enemy" and "Friend".
    // Enemies and Friends can overlap.
    // The two layers colliding have been disabled.
    // Edit > Project Settings > Physics 2D.
    // --------------------------------------------------------------------------
    // Same as "virtual void", but has to be called in Child classes.
    // Since added "abstract void" here, have to add "abstract" at start of class.
    protected abstract void OnDrawGizmos();

    protected virtual void Shoot() { }

    protected virtual void StatusSuffocate()
    {
        if (Suffocate == true) { EffectSuffocate.SetActive(true); }
        else { EffectSuffocate.SetActive(false); }
    }

    protected virtual void StatusPoisoned()
    {
        if (Poisoned == true) { EffectPoisoned.SetActive(true); }
        else { EffectPoisoned.SetActive(false); }
    }

    protected virtual void StatusBurned()
    {
        if (Burned == true) { EffectBurned.SetActive(true); }
        else { EffectBurned.SetActive(false); }
    }
}