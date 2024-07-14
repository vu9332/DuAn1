using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeathHealth : MonoBehaviour,IDamageAble
{
    [SerializeField] private float Health;
    public float health { get; set; }
    public float currentHealth { get; set; }
    private void Start()
    {
        health = Health;
        currentHealth = health;
    }
    //Nếu va chạm với kiếm của Player thì quái sẽ bị trừ máu
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Máu boss còn: " + currentHealth);
    }
    //Nếu máu về 0
    public void Die()
    {
        if (currentHealth <= 0)
        {

        }
    }
}
