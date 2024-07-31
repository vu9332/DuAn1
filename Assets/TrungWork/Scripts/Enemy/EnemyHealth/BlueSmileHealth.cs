using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSmileHealth : EnemyAIHealth
{
    [SerializeField] private BlueSmileSC blueSmileSC;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        health = blueSmileSC.health;
        currentHealth = health;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        base.Die();
        pl.playerExp += blueSmileSC.experience;
        Level.levelInstance.UpLevelIfPlayerGotFull(pl.playerExp);
    }
}
