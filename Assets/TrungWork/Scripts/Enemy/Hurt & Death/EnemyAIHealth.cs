using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHealth : Enemy,IDamageAble
{
    [SerializeField] protected float knockBackThrust;
    [Header("Effect")]
    [SerializeField] protected GameObject hurtSFX;
    [SerializeField] protected GameObject deathSFXBlue;
    [SerializeField] protected GameObject deathSFXBlack;
    [SerializeField] protected GameObject deathSFXPurple;
    [Header("Sound Effect")]
    [SerializeField] protected AudioClip snd_death;
    protected Flash flash;
    protected KnockBack knockBack;
    protected Collider2D coll;
    public bool isHurting=false;
    public bool isDead = false;
    protected override void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack=GetComponent<KnockBack>();
        coll=GetComponent<Collider2D>();
    }
    //Nếu va chạm với kiếm của Player thì quái sẽ bị trừ máu
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        isHurting = true;
        StartCoroutine(flash.FlashRoutine());
        knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    //Nếu máu về 0
    public override void Die()
    {
        coll.enabled = false;
        isDead = true;
        if (!gameObject.GetComponent<Death>())
        {
            Destroy(gameObject);
        }
        SoundFXManagement.Instance.PlaySoundFXClip(snd_death, transform, 100);
        PlayEffect();
    }
    private void PlayEffect()
    {
        if (deathSFXBlue != null)
        {
            Instantiate(deathSFXBlue, transform.position, Quaternion.identity);
        }
        if (deathSFXBlack != null)
        {
            Instantiate(deathSFXBlack, transform.position, Quaternion.identity);
        }
        if (deathSFXPurple != null)
        {
            Instantiate(deathSFXPurple, transform.position, Quaternion.identity);
        }
        if (hurtSFX != null)
        {
            Instantiate(hurtSFX, transform.position, Quaternion.identity);
        }
    }
}
