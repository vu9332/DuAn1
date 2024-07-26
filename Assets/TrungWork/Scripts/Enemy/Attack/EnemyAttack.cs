using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float damageAttack;
    private PlayerHealth playerHealth;
    public bossFlyingEyes bossFlyingEyes;
    private void Awake()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void Start()
    {
        damageAttack = bossFlyingEyes.damage;
    }
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        //Nếu Quai trúng Player thì trừ máu Player
        if (enemy.gameObject.GetComponent<PlayerController>())
        {
            playerHealth.TakeDamage(damageAttack);
            Debug.Log("Đã va chạm Player, Player con: "+playerHealth.currentHealth);
        }
    }
}
