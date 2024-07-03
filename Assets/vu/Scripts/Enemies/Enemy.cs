using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageAble
{
    // Start is called before the first frame update
   

    public float health { get ; set ; }
    public float currentHealth { get ; set; }

    void Start()
    {
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
