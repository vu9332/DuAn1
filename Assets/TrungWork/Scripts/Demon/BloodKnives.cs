using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKnives : MonoBehaviour
{
    [SerializeField] private bossDemon bossDemon;
    private PlayerHealth playerHealth;
    private float damageKnives;
    private void Awake()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void Start()
    {
        damageKnives = bossDemon.damageAttack;
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.GetComponent<PlayerHealth>() != null)
        {
            playerHealth.TakeDamage(damageKnives);
            Debug.Log("Mau Player con: " + playerHealth.currentHealth);
        }
    }
}
