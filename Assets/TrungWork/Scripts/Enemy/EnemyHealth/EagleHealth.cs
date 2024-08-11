using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleHealth : EnemyAIHealth
{
    private Animator anim;
    [SerializeField] private EagleSC eagleSC;
    protected override void Awake()
    {
        anim= GetComponent<Animator>();
        base.Awake();
    }
    private void Start()
    {
        health = eagleSC.health;
        currentHealth = health;
    }
    public override void TakeDamage(float damage)
    {
        anim.SetTrigger("Hurt");
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        base.Die();
        pl.playerExp += eagleSC.experience;
        Rewards.rewardInstance.GiveRewardToPlayer(Rewards.rewardInstance.currency, transform, eagleSC.coins);
        Rewards.rewardInstance.GiveRewardToPlayer(Rewards.rewardInstance.currency, transform, eagleSC.hearts);
        CharacterEvents.characterTookExp.Invoke(UIManager.UIManagerInstance.ExpTextPrefab, PlayerController.Instance.gameObject, eagleSC.experience);
    }
}
