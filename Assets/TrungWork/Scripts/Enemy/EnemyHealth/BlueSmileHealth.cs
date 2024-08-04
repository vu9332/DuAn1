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
        Rewards.rewardInstance.GiveRewardToPlayer(Rewards.rewardInstance.currency, transform, blueSmileSC.coins);
        CharacterEvents.characterTookExp.Invoke(UIManager.UIManagerInstance.ExpTextPrefab, PlayerController.Instance.gameObject, blueSmileSC.experience);
    }
}
