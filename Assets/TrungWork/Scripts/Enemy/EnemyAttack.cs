using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    private PlayerHealth playerHealth;
    private void Awake()
    {
        playerHealth = GameObject.FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        //Nếu Quai trúng Player thì trừ máu Player
        if (enemy.gameObject.GetComponent<PlayerController>())
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Đã va chạm Player, Player con: "+playerHealth.currentHealth);
        }
    }
}
