using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHealth : BringerOfDeathHealth
{
    [SerializeField] private bossDemon bossDemon;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        health = bossDemon.health;
        currentHealth = health;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}
