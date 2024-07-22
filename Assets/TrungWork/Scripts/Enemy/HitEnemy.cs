using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    [SerializeField] private LayerMask layerEnemies;
    private Collider2D coll;
    private bool takeHit = false;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {
        if(!takeHit)
        {
            Collider2D[] HitsEnemies = Physics2D.OverlapCircleAll(transform.position, 2, layerEnemies);
            foreach (var enemy in HitsEnemies)
            {
                if (enemy.GetComponent<EnemyAIHealth>() != null)
                {
                    EnemyAIHealth t = enemy.GetComponent<EnemyAIHealth>();
                    t.TakeDamage(10);
                    takeHit = true;
                    break;
                }
                if (enemy.GetComponent<DemonHealth>() != null)
                {
                    DemonHealth a = enemy.GetComponent<DemonHealth>();
                    a.TakeDamage(10);
                    StartCoroutine(Restore());
                    takeHit = true;
                    break;
                }
                if (enemy.GetComponent<EnemyHealth>() != null)
                {
                    EnemyHealth b = enemy.GetComponent<EnemyHealth>();
                    b.TakeDamage(10);
                    takeHit = true;
                    StartCoroutine(Restore());
                    break;
                }
            }
        }
    }
    IEnumerator Restore()
    {
        yield return null;
        takeHit = false;
    }
}
