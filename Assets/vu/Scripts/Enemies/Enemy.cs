using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageAble
{
    // Start is called before the first frame update
   public static Enemy Instance { get; set; }

    public float health { get ; set ; }
    public float currentHealth { get ; set; }

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
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("curenthealt" + currentHealth);
    }

    public void Die()
    {
       
    }
}
