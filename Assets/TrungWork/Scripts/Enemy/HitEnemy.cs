using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.GetComponent<EnemyAIHealth>())
        {
            EnemyAIHealth enemyAIHealth=player.gameObject.GetComponent<EnemyAIHealth>();
            enemyAIHealth.TakeDamage(10);
        }
    }
}
