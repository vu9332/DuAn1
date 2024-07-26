using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeHealth : EnemyAIHealth
{
    [SerializeField] private GrapeProjectitle grape;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        health = grape.health;
        currentHealth = health;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        base.Die();
    }
}
