using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmileHealth : EnemyAIHealth
{
    [SerializeField] private BlackSmileSC blackSmileSC;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        health = blackSmileSC.health;
        currentHealth = health;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        base.Die();
        pl.playerExp += blackSmileSC.experience;
    }
}
