using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack1 : MonoBehaviour
{
    [SerializeField] private float distance;
    private SpriteRenderer sp;
    private bool isAttacking=false;
    private Rigidbody2D rb;
    private void Awake()
    {
        sp=GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position,PlayerController.Instance.transform.position) < distance)
        {
            isAttacking=true;
        }
        else
        {
            isAttacking=false;
        }
        if (isAttacking)
        {
            if (transform.position.x < PlayerController.Instance.transform.position.x)
            {
                sp.flipX = false;
                rb.velocity = new Vector2(rb.velocity.x, 3);
            }
            else
            {
                sp.flipX = true;
                rb.velocity = new Vector2(rb.velocity.x, 3);
            }
        }
    }
}
