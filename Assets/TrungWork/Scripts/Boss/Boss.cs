using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] protected float walkSpeed;
    protected Rigidbody2D rb;
    public DetectionZone attackZone;
    protected Animator animator;
    protected Transform playerPosition;
    protected Vector2 directionTarget;
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
                }
                else if (value == WalkableDirection.Left)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
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
    protected virtual void Awake()
    {
        playerPosition = FindAnyObjectByType<PlayerController>().GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public void Move()
    {
        directionTarget = new Vector2(playerPosition.position.x, transform.position.y);
        directionTarget.Normalize();
        rb.MovePosition(rb.position + directionTarget * walkSpeed * Time.fixedDeltaTime);
        SwitchBossDirection();
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
