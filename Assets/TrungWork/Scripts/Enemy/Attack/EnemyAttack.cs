using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private int ID = 1;
    public BossesManager bossesManager;
    private int indexBoss;

    private float damage;
    private PlayerHealth playerHealth;
    private void Awake()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void Start()
    {
        if (bossesManager != null)
        {
            damage = bossesManager.listBosses[0].damage;
        }
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
