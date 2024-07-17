using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DemonHealth : MonoBehaviour
{
    Animator animator;
    [SerializeField] private float _maxHealth;
    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField] private float _health;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    [SerializeField] private bool _isAlive = true;
    [SerializeField] private bool IsInvincible = false;
    private float timeSinceHit = 0;
    private float invincibilityTimer = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("Is Alive Set + " + value);
        }
    }

    public bool IsHit
    {
        get
        {
            return animator.GetBool(AnimationStrings.isHit);
        }
        private set
        {
            animator.SetBool(AnimationStrings.isHit, value);
        }

    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (IsInvincible)
        {
            if (timeSinceHit > invincibilityTimer)
            {
                IsInvincible= false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }
    public void TakeDamage(float damage)
    {
        if (IsAlive && !IsInvincible)
        {
            IsHit = true;
            Health -= damage;
            IsInvincible = true;
            Debug.Log("Da trung Player!");
        }
    }
}
