using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BringerOfDeathHealth : MonoBehaviour,IDamageAble
{
    [Header("UI")]
    [SerializeField] private GameObject healthBarObject;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI textBoss;
    [SerializeField] private float Health;
    [SerializeField] private float knockBackThrust;
    private Animator anim;
    private Collider2D coll;
    private KnockBack knockBack;
    private Rigidbody2D rb;
    public float health { get; set; }
    public float currentHealth { get; set; }
    private void Awake()
    {
        anim=GetComponent<Animator>();
        coll=GetComponent<Collider2D>();
        knockBack=GetComponent<KnockBack>();
        rb=GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        health = Health;
        currentHealth = health;
        healthBarObject.SetActive(true);
        textBoss.text = "BOSS";
    }
    //Nếu va chạm với kiếm của Player thì quái sẽ bị trừ máu
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hurt");
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / Health;
        Debug.Log("Máu boss còn: " + currentHealth);
        Die();
    }
    //Nếu máu về 0
    public void Die()
    {
        if (currentHealth <= 0)
        {
            anim.SetTrigger("Death");
            coll.enabled= false;
        }
    }
}
