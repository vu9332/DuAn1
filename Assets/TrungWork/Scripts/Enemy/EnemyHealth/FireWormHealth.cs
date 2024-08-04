using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWormHealth : EnemyAIHealth
{
    [SerializeField] private FireWormSC fireWormSC;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        health = fireWormSC.health;
        currentHealth = health;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        pl.playerExp += fireWormSC.experience;
        Rewards.rewardInstance.GiveRewardToPlayer(Rewards.rewardInstance.currency, transform, fireWormSC.coins);
        CharacterEvents.characterTookExp.Invoke(UIManager.UIManagerInstance.ExpTextPrefab, PlayerController.Instance.gameObject, fireWormSC.experience);
        base.Die();
    }
}
