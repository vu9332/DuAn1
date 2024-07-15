using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHealth : MonoBehaviour,IDamageAble
{
    [SerializeField] private float knockBackThrust;
    [SerializeField] private float Health;
    [Header("Effect")]
    [SerializeField] private GameObject hurtSFX;
    [SerializeField] private GameObject deathSFXBlue;
    [SerializeField] private GameObject deathSFXBlack;
    [SerializeField] private GameObject deathSFXPurple;
    [Header("Sound Effect")]
    [SerializeField] private AudioClip snd_death;
    private Flash flash;
    private KnockBack knockBack;
    private Collider2D coll;
    public bool isHurting=false;
    public bool isDead = false;
    public float health { get; set; }
    public float currentHealth { get; set; }
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack=GetComponent<KnockBack>();
        coll=GetComponent<Collider2D>();
    }
    private void Start()
    {
        health = Health;
        currentHealth = health;
    }
    //Nếu va chạm với kiếm của Player thì quái sẽ bị trừ máu
    public void TakeDamage(float damage)
    {
        isHurting = true;
        currentHealth-=damage;
        StartCoroutine(flash.FlashRoutine());
        knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        Die();
    }
    //Nếu máu về 0
    public void Die()
    {
        if (currentHealth <= 0)
        {
            coll.enabled=false;
            isDead = true;
            if (!gameObject.GetComponent<Death>())
            {
                Destroy(gameObject);
            }
            SoundFXManagement.Instance.PlaySoundFXClip(snd_death, transform, 100);
            PlayEffect();
        }
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
