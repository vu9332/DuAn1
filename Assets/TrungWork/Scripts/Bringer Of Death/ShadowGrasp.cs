using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowGrasp : MonoBehaviour
{
    [SerializeField] private float damage;
    private PlayerHealth playerHealth;
    private void Awake()
    {
        playerHealth=GameObject.FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.GetComponent<PlayerController>())
        {
            if (player!= null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Nhan vat da bi trung skill, mau con: "+playerHealth.currentHealth);
            }
        }
    }
}
