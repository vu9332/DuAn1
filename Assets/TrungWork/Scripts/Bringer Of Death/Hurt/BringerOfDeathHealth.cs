﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BringerOfDeathHealth : Enemy
{
    [SerializeField] private bossBringerOfDeath bossBringerOfDeath;
    protected Animator animator;
    protected KnockBack knockBack;
    [SerializeField] protected Image healthBar;
    [SerializeField] private float knockBackThrust;
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
        knockBack=GetComponent<KnockBack>();
    }
    private void Start()
    {
        AudioManager.Instance.PlayMusicSFX(AudioManager.Instance.Level2);
        health = bossBringerOfDeath.health;
        currentHealth= health;
    }
    private void Update()
    {
        
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
        else
        {
            knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        }
    }
    public override void Die()
    {
        AudioManager.Instance.StopMusicSFX(AudioManager.Instance.Level2);
        pl.playerExp += bossBringerOfDeath.amountExperiencesReceived;
        Level.levelInstance.UpLevelIfPlayerGotFull(pl.playerExp);
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
    void BossIsDefeated()
    {
        AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_boss_Defeated);
    }
}
