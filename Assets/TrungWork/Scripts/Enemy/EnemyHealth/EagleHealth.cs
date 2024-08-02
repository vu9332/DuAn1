using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleHealth : EnemyAIHealth
{
    [SerializeField] private EagleSC eagleSC;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        health = eagleSC.health;
        currentHealth = health;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        base.Die();
        pl.playerExp += eagleSC.experience;
    }
}
