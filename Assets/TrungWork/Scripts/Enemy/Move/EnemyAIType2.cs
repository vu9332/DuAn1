using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIType2 : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private Transform posLeft;
    [SerializeField] private Transform posRight;
    [Header("Move")]
    [SerializeField] private float speedMove;
    [SerializeField] private float speedJump;
    [Header("Other")]
    [SerializeField] private LayerMask layerGround;
    private Collider2D coll;
    private bool facingLeft = true;
    private Rigidbody2D rb;
    private enum State { idle, jump, fall}
    private State state;
    private Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll= GetComponent<Collider2D>();
        anim=GetComponent<Animator>();
    }
    private void Update()
    {
        if (transform.position.x < posLeft.position.x)
        {
            facingLeft = false;
            Flip();
        }
        if (transform.position.x > posRight.position.x)
        {
            facingLeft = true;
            Flip();
        }
        Move();
    }
    private void Move()
    {
        if (coll.IsTouchingLayers(layerGround) && state == State.idle)
        {
            if (facingLeft)
            {
                rb.velocity = new Vector2(-speedMove, speedJump);
                state = State.jump;
            }
            else
            {
                rb.velocity = new Vector2(speedMove, speedJump);
                state = State.jump;
            }
        }
        StateSwtich();
        anim.SetInteger("state", (int)state);
    }
    private void Flip()
    {
        if (facingLeft)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void StateSwtich()
    {
        if (state == State.jump)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.fall;
            }
        }
        else if (state == State.fall)
        {
            if (coll.IsTouchingLayers(layerGround))
            {
                state = State.idle;
            }
        }
    }
}
