using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeBitePlayer : MonoBehaviour
{
    [SerializeField] private float damageBite;
    private PlayerHealth playerHealth;
    private void Awake()
    {
        playerHealth = GameObject.FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.GetComponent<PlayerController>())
        {
            playerHealth.TakeDamage(damageBite);
            Debug.Log("Player đã bị cắn, máu Player còn: "+playerHealth.currentHealth);
        }
    }
}
