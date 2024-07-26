using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyingEyesDamage : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private Animator anim;
    [SerializeField] private float damage;
    private void Awake()
    {
        anim = GameObject.FindAnyObjectByType<EnemyHealth>().GetComponent<Animator>();
        enemyHealth = GameObject.FindAnyObjectByType<EnemyHealth>().GetComponent<EnemyHealth>();
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<EnemyHealth>() != null)
        {
            //anim.SetTrigger("StartFight");
            enemyHealth.TakeDamage(damage);
        }
    }
}
