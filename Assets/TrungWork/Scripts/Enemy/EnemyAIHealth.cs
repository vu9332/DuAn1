using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHealth : MonoBehaviour,IDamageAble
{
    [SerializeField] private float knockBackThrust;
    [SerializeField] private float Health;
    [SerializeField] private GameObject hurtSFX;
    [SerializeField] private GameObject hurtSFXBlue;
    private Flash flash;
    private KnockBack knockBack;
    public float health { get; set; }
    public float currentHealth { get; set; }
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack=GetComponent<KnockBack>();
    }
    private void Start()
    {
        health = Health;
        currentHealth = health;
    }
    //Nếu va chạm với kiếm của Player thì quái sẽ bị trừ máu
    public void TakeDamage(float damage)
    {
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
            Destroy(gameObject);
            PlayEffect();
        }
    }
    private void PlayEffect()
    {
        if (hurtSFXBlue != null)
        {
            Instantiate(hurtSFXBlue, transform.position, Quaternion.identity);
        }
        if (hurtSFX != null)
        {
            Instantiate(hurtSFX, transform.position, Quaternion.identity);
        }
    }
}
