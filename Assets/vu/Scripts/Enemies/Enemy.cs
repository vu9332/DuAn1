using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageAble
{
    // Start is called before the first frame update
    public static Enemy Instance;

    public float health { get ; set ; }
    public float currentHealth { get ; set; }
    private int amountExperiencesReceived;
    protected Collider2D colliderBoss;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        colliderBoss = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;    
        }
        currentHealth = health;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("curenthealt" + currentHealth);
    }

    public virtual void Die()
    {
        colliderBoss.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    //Tăng điểm kinh nghiệm
    public virtual void GetAmountExperience(int _amountExperiencesReceived)
    {
        amountExperiencesReceived = _amountExperiencesReceived;
    }
    public virtual int AddExperiences(int currentExperiencePlayer)
    {
        Rewards.rewardInstance.GiveExperienceToPlayer(currentExperiencePlayer, amountExperiencesReceived);
        return currentExperiencePlayer;
    }
}
