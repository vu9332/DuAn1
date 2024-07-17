using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyesFall : MonoBehaviour
{
    public ContactFilter2D contactFilter;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    BoxCollider2D touchingBoxCol;
    public bool deathTouchingGround = false;
    [SerializeField] private float groundDistance;
    private EnemyHealth enemyHealth;
    private void Awake()
    {
        touchingBoxCol = GetComponent<BoxCollider2D>();
        enemyHealth= GetComponent<EnemyHealth>();
    }
    private void FixedUpdate()
    {
        deathTouchingGround=touchingBoxCol.Cast(Vector2.down,contactFilter,groundHits,groundDistance) > 0;
    }
}
