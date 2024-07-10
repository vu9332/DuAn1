using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDamage : MonoBehaviour
{
    [SerializeField] private float damageSpray;
    private PlayerHealth playerHealth;
    void Awake()
    {
        playerHealth= GameObject.FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        //Nếu đạn chạm với Player thì trừ máu Player
        if(player.gameObject.GetComponent<PlayerHealth>())
        {
            playerHealth.TakeDamage(damageSpray);
            Debug.Log("Player còn: " + playerHealth.currentHealth);
        }
    }
}
