using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D),typeof(TouchDirection))]
public class Boss : MonoBehaviour
{
    [SerializeField] protected float walkSpeed;
    protected Rigidbody2D rb;
    public DetectionZone attackZone;
    protected Animator animator;
    protected Transform playerPosition;
    protected TouchDirection touchDirection;
    public enum WalkableDirection { Right, Left }
    protected WalkableDirection _walkDirection;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                if (value == WalkableDirection.Right)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    protected bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationBoss.hasTarget, value);
        }
    }
    public bool IsHit
    {
        get
        {
            return animator.GetBool(AnimationBoss.isHit);
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationBoss.canMove);
        }
    }

    protected Vector2 walkDirectionVector = Vector2.left;
    protected virtual void Awake()
    {
        playerPosition = FindAnyObjectByType<PlayerController>().GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchDirection=GetComponent<TouchDirection>();
    }
    public void Move()
    {
        if(rb.bodyType!= RigidbodyType2D.Static)
        {
            rb.velocity = new Vector2(walkDirectionVector.x * walkSpeed, rb.velocity.y);
            SwitchBossDirection();
        }
    }
    protected virtual void FindCollider()
    {
        HasTarget = attackZone.detectionColliders.Count > 0;
    }
    protected virtual void SwitchBossDirection()
    {
        if (transform.position.x < playerPosition.position.x)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else if (transform.position.x > playerPosition.position.x)
        {
            WalkDirection = WalkableDirection.Left;
        }
    }
}
