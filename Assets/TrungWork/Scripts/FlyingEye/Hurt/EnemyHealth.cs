using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class EnemyHealth : Enemy, IDamageAble
{
    [Header("UI")]
    [SerializeField] protected GameObject healthBarObject;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected TextMeshProUGUI textBoss;
    [SerializeField] protected GameObject NoticeYouKilledBoss;
    [Header("Effect")]
    [SerializeField] private GameObject deathSFX;
    [SerializeField] private GameObject hurtSFX;
    [SerializeField] private GameObject explosionSFX;
    [Header("Sound")]
    [SerializeField] private AudioClip bossDefeat;
    [SerializeField] private AudioClip bossExplosion;
    [SerializeField] private AudioClip bossGushing;
    [SerializeField] private AudioClip bossHurting;
    [SerializeField] private AudioClip bossBiting;

    [Header("Other")]
    [SerializeField] private GameObject deathFall;
    private Flash flash;
    private Animator anim;
    private CapsuleCollider2D capsuleCollider2D;
    private EffectFall effectFall;
    private FlyingEyes flyingEyes;
    private FlyingEyesFall flyingEyesFall;
    private bool isStartFight = false;
    private int countHit = 0;
    public bool isDead = false;
    public bool BossIsDead = false;
    public bossFlyingEyes bossFlyingEyes;

    protected override void Awake()
    {
        flash = GetComponent<Flash>();
        anim = GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();  
        effectFall = GetComponent<EffectFall>();
        flyingEyes = GetComponent<FlyingEyes>();
        flyingEyesFall = GetComponentInChildren<FlyingEyesFall>();
    }
    private void Start()
    {
        health=bossFlyingEyes.health;
        currentHealth = health;
        healthBarObject.SetActive(false);
        textBoss.text = null;
        NoticeYouKilledBoss.SetActive(false);
        deathFall.SetActive(false);
    }
    private void Update()
    {
        if (isDead)
        {
            deathFall.SetActive(true);
            effectFall.radius = 0;
            if (!BossIsDead)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Static;
                flyingEyes.groundCheckDownRadius = 0;
            }
            if (flyingEyesFall.deathTouchingGround)
            {
                BossIsDead = true;
                flyingEyes.groundCheckDownRadius = 0;
            }
        }
    }
    //Nếu va chạm với kiếm của Player thì quái sẽ bị trừ máu
    public override void TakeDamage(float damage)
    {
        ++countHit;
        if (!isStartFight)
        {
            isStartFight = true;
            healthBarObject.SetActive(true);
            textBoss.text = "BOSS";
            anim.SetTrigger("StartFight");
            SoundFXManagement.Instance.PlaySoundFXClip(bossHurting, transform, 100);
            Instantiate(hurtSFX, transform.position, Quaternion.identity);
            AudioManager.Instance.PlayMusicSFX(AudioManager.Instance.Level1);
        }
        if (isStartFight && countHit > 1)
        {
            SoundFXManagement.Instance.PlaySoundFXClip(bossHurting, transform, 100);
            Instantiate(hurtSFX, transform.position, Quaternion.identity);
            base.TakeDamage(damage);
            healthBar.fillAmount = currentHealth / health;
            Debug.Log("Máu boss còn: " + currentHealth);
            StartCoroutine(flash.FlashRoutine());
            Die();
        }
    }
    //Nếu máu về 0
    public override void Die()
    {
        if(currentHealth <= 0)
        {
            AudioManager.Instance.StopMusicSFX(AudioManager.Instance.Level1);
            if (FlyingEyes.Instance.Body.activeInHierarchy || FlyingEyes.Instance.Bite.activeInHierarchy)
            {
                FlyingEyes.Instance.Body.SetActive(false);
                FlyingEyes.Instance.Bite.SetActive(false);
            }
            capsuleCollider2D.enabled = false;
            healthBarObject.SetActive(false);
            textBoss.text = null;
            anim.SetTrigger("Death");
            StartCoroutine(StartDeathSFX());
        }
    }
    void Dead()
    {
        isDead = true;
        StartCoroutine(YouKilledBoss());
    }
    IEnumerator YouKilledBoss()
    {
        yield return new WaitForSeconds(2);
        NoticeYouKilledBoss.SetActive(true);
        yield return new WaitForSeconds(5);
        NoticeYouKilledBoss.SetActive(false);
        pl.playerExp += bossFlyingEyes.amountExperiencesReceived;
    }
    IEnumerator StartDeathSFX()
    {
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.5f);
        SoundFXManagement.Instance.PlaySoundFXClip(bossGushing, transform, 100);
        Instantiate(deathSFX, transform.position, Quaternion.identity);
    }
    void BossDead()
    {
        SoundFXManagement.Instance.PlaySoundFXClip(bossExplosion, transform, 100);
    }
    void BossWasDefeated()
    {
        SoundFXManagement.Instance.PlaySoundFXClip(bossDefeat,transform,100);
    }
    void Explosion()
    {
        Instantiate(explosionSFX, transform.position, Quaternion.identity);
    }
    void BitePlayer()
    {
        SoundFXManagement.Instance.PlaySoundFXClip(bossBiting, transform, 100);
    }
}
