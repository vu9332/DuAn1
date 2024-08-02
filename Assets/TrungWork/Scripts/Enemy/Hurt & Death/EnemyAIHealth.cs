using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIHealth : Enemy,IDamageAble
{
    public static EnemyAIHealth EAHInstance;
    [SerializeField] protected float knockBackThrust;
    [Header("Effect")]
    [SerializeField] protected GameObject hurtSFX;
    [SerializeField] protected GameObject deathSFXBlue;
    [SerializeField] protected GameObject deathSFXBlack;
    [SerializeField] protected GameObject deathSFXPurple;
    [Header("Sound Effect")]
    [SerializeField] protected AudioClip snd_death;

    [Header("UI")]
    [SerializeField] protected Image healthBarEnemy;
    protected Flash flash;
    protected KnockBack knockBack;
    protected Collider2D coll;
    protected AlmostDead almostDead;
    public bool isHurting=false;
    public bool isDead = false;
    protected override void Awake()
    {
        EAHInstance = this;
        flash = GetComponent<Flash>();
        knockBack=GetComponent<KnockBack>();
        coll=GetComponent<Collider2D>();
        rb=GetComponent<Rigidbody2D>();
        almostDead=GetComponent<AlmostDead>();
    }
    //Nếu va chạm với kiếm của Player thì quái sẽ bị trừ máu
    public override void TakeDamage(float damage)
    {
        CharacterEvents.characterDamaged.Invoke(gameObject,damage);
        base.TakeDamage(damage);
        isHurting = true;
        StartCoroutine(flash.FlashRoutine());
        knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        healthBarEnemy.fillAmount = (float)currentHealth / health;
        if (currentHealth <= 0)
        {
            Die();
        }
        StartCoroutine(a());
        if (currentHealth <= 5 && currentHealth >0)
        {
            StartCoroutine(almostDead.FlashRoutine());
        }
    }
    IEnumerator a()
    {
        yield return new WaitForSeconds(0.5f);
        isHurting = false;
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
