using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack3 : MonoBehaviour
{
    [SerializeField] private float damage;
    private PlayerHealth playerHealth;
    private bool isHitPlayer = false;
    private void Awake()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerHealth>() != null && !isHitPlayer)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Mau Player con: " + playerHealth.currentHealth);
            isHitPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>() != null && isHitPlayer)
        {
            isHitPlayer = false;
        }
    }
}