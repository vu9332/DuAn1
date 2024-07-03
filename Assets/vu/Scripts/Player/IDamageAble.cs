using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageAble 
{
   public void TakeDamage(float damage);

    public float health { get; set; }
    public float currentHealth { get; set; }

    public void Die();
    
}
