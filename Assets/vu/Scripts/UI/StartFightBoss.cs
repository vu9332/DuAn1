using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFightBoss : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            EnemyHealth.Instance.StartFighting();
        }
        
    }
}
