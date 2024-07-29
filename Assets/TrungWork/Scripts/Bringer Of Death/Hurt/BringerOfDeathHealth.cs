using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BringerOfDeathHealth : Enemy
{
    [SerializeField] private bossBringerOfDeath bossBringerOfDeath;
    [SerializeField] protected Image healthBar;
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
        healthBar.fillAmount = (float)currentHealth / health;
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
        base.GetAmountExperience(bossBringerOfDeath.amountExperiencesReceived);
        animator.SetTrigger("Death");
        animator.SetBool(AnimationBoss.isAlive, false);
    }
    //Nhận thưởng khi boss chết
    void Reward()
    {
        Rewards.rewardInstance.GiveRewardToPlayer(Rewards.rewardInstance.chest, transform, 1);
    }
    //SoundSFX
    void BringerOfDeathDeathSFX()
    {
        AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_bringer_death);
    }
    void BringerOfDeathHurtSFX()
    {
        AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_bringer_hurt);
    }
}
