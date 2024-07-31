using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack3 : MonoBehaviour
{
    [SerializeField] private DinoSC dinoSC;
    private float damage;
    //private PlayerHealth playerHealth;
    private bool isHitPlayer = false;
    private void Awake()
    {
       // playerHealth = FindAnyObjectByType<PlayerHealth>().GetComponent<PlayerHealth>();
    }
    private void Start()
    {
        damage = dinoSC.damageAttack;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerHealth>() != null && !isHitPlayer)
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.PlayerTakeDamage(damage, this.transform);
                //PlayerHealth.Instance.PlayerTakeDamage(damage,this.gameObject.transform);
                Debug.Log("Mau Player con: " + playerHealth.currentHealth);
                isHitPlayer = true;

            }
           
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
