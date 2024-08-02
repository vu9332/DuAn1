using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoHealth : EnemyAIHealth
{
    [SerializeField] private DinoSC dinoSC;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        health = dinoSC.health;
        currentHealth = health;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        base.Die();
        pl.playerExp += dinoSC.experience;
    }
}
