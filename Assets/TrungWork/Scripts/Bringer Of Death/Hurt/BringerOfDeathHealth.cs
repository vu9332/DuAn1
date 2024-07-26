using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeathHealth : Enemy
{
    [SerializeField] private bossBringerOfDeath bossBringerOfDeath;
    protected Animator animator;
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationBoss.isAlive);
        }
    }
    protected override void Awake()
    {
        base.Awake();
        animator=GetComponent<Animator>();
    }
    private void Start()
    {
        health = bossBringerOfDeath.health;
        currentHealth= health;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        animator.SetBool(AnimationBoss.isHit,true);
        Debug.Log("Mau boss con: "+currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public override void Die()
    {
        base.Die();
        animator.SetTrigger("Death");
        animator.SetBool(AnimationBoss.isAlive, false);
    }
}
