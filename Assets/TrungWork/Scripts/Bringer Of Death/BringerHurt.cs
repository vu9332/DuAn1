using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerHurt : MonoBehaviour
{
    private BringerOfDeathHealth enemyHealth;
    private void Awake()
    {
        enemyHealth = GameObject.FindAnyObjectByType<BringerOfDeathHealth>().GetComponent<BringerOfDeathHealth>();
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<BringerOfDeathHealth>() != null)
        {
            //anim.SetTrigger("StartFight");
            enemyHealth.TakeDamage(10);
        }
    }
}
