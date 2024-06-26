using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, TakeDamage
{
    // Start is called before the first frame update
    public float maxHealth;
    public float curentHealth;
    void Start()
    {
        curentHealth=maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        curentHealth-=damage;
        Debug.Log("curenthealt" + curentHealth);
    }

}
