using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DemonHealth : BringerOfDeathHealth
{
    [SerializeField] private bossDemon bossDemon;
    protected override void Awake()
    {
        base.Awake();
        //Instantiate(bossDemon.healthBar,bossDemon.healthBarPosition.position, Quaternion.identity);
    }
    private void Start()
    {
        health = bossDemon.health;
        currentHealth = health;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthBar.fillAmount = (float)currentHealth/health;
    }
    public override void Die()
    {
        colliderBoss.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        base.GetAmountExperience(bossDemon.amountExperiencesReceived);
        animator.SetTrigger("Death");
        animator.SetBool(AnimationBoss.isAlive, false);
    }
}
