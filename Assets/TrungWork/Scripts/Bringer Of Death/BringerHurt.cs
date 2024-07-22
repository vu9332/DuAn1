using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerHurt : MonoBehaviour
{
    private DemonHealth enemyHealth;
    private void Awake()
    {
        enemyHealth = GameObject.FindAnyObjectByType<DemonHealth>().GetComponent<DemonHealth>();
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<DemonHealth>() != null)
        {
            //anim.SetTrigger("StartFight");
            enemyHealth.TakeDamage(10);
        }
    }
}
