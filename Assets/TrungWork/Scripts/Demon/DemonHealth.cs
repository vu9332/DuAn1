using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class DemonHealth : BringerOfDeathHealth
{
    [SerializeField] private bossDemon bossDemon;
    [SerializeField] private PlayerData playData;
    [SerializeField] private GameObject Bar;
    [SerializeField] private GameObject panelWin;

    
    protected override void Awake()
    {
        base.Awake();
        //Instantiate(bossDemon.healthBar,bossDemon.healthBarPosition.position, Quaternion.identity);
    }
    private void Start()
    {
        panel.SetActive(false);
        AudioManager.Instance.PlayMusicSFX(AudioManager.Instance.Level3);
        health = bossDemon.health;
        currentHealth = health;
    }
    private void Update()
    {
        if (playData.playerCurrentHealth <= 0)
        {
            healthBar.gameObject.SetActive(false);
        }
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        panelWin.SetActive(true);
        StartCoroutine(DisplayTextExp());
        panel.SetActive(true);
        AudioManager.Instance.StopMusicSFX(AudioManager.Instance.Level3);
        AudioManager.Instance.StopMusicSFX(AudioManager.Instance.snd_hellball);
        AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_demon_death);
        colliderBoss.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Death");
        animator.SetBool(AnimationBoss.isAlive, false);
    }
    IEnumerator DisplayTextExp()
    {
        yield return new WaitForSeconds(3f);
        pl.playerExp += bossDemon.amountExperiencesReceived;
        CharacterEvents.characterTookExp.Invoke(UIManager.UIManagerInstance.ExpTextPrefab, PlayerController.Instance.gameObject, bossDemon.amountExperiencesReceived);
    }
    void BossIsDefeated()
    {
        AudioManager.Instance.PlaySoundSFX(AudioManager.Instance.snd_boss_Defeated);
        Rewards.rewardInstance.GiveRewardToPlayer(Rewards.rewardInstance.currency, transform, bossDemon.amountCoinsReveived);
    }
    
}
