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
        Rewards.rewardInstance.GiveRewardToPlayer(Rewards.rewardInstance.currency, transform, blackSmileSC.coins);
        pl.playerExp += blackSmileSC.experience;
        CharacterEvents.characterTookExp.Invoke(UIManager.UIManagerInstance.ExpTextPrefab, PlayerController.Instance.gameObject, blackSmileSC.experience);
    }
}
