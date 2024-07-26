using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowGrasp : MonoBehaviour
{
    [SerializeField] private bossBringerOfDeath bossBringerOfDeath;
    private PlayerHealth playerHealth;
    private float damageBossSkill;
    private void Awake()
    {
        playerHealth=GameObject.FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void Start()
    {
        damageBossSkill = bossBringerOfDeath.damageSkill;
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.GetComponent<PlayerController>())
        {
            if (player!= null)
            {
                playerHealth.TakeDamage(damageBossSkill);
                Debug.Log("Nhan vat da bi trung skill, mau con: "+playerHealth.currentHealth);
            }
        }
    }
}
