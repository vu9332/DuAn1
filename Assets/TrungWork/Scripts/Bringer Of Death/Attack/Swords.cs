using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : MonoBehaviour
{
    [SerializeField] private bossBringerOfDeath bossBringerOfDeath;
    private PlayerHealth playerHealth;
    private float damageSwords;
    private void Awake()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void Start()
    {
        damageSwords=bossBringerOfDeath.damageAttack;
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.GetComponent<PlayerHealth>() != null)
        {
            playerHealth.TakeDamage(damageSwords);
            Debug.Log("Mau Player con: " + playerHealth.currentHealth);
        }
    }
}
