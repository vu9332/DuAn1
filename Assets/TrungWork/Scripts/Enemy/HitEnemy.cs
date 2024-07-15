using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    [SerializeField] private LayerMask layerEnemies;
    private Collider2D coll;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {
        Collider2D[] HitsEnemies = Physics2D.OverlapCircleAll(transform.position, 2, layerEnemies);
        foreach (var enemy in HitsEnemies)
        {
            EnemyHealth t =enemy.GetComponent<EnemyHealth>();
            t.TakeDamage(10);
        }
    }
}