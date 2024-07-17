using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float damage;
    private PlayerHealth playerHealth;
    private void Awake()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>())
        {
            playerHealth.TakeDamage(damage);

            Debug.Log("Da trung Player!");
        }
    }
}
