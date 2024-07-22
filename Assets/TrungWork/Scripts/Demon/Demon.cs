using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchDirection))]
public class Demon : MonoBehaviour
{
    public DetectionZone attackZone;
    public float walkSpeed = 3f;
    public float walkStopRate = 1.0f;
    Rigidbody2D rb;
    TouchDirection touchDirection;
    Animator animator;
    Transform playerPos;
    public enum WalkableDirection { Right,Left }
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector;
    private int direction=0;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                //gameObject.transform.localScale = new Vector2(transform.localScale.x * -1,transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0,180,0);
                }
                else if (value == WalkableDirection.Left)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            _walkDirection = value;
        }
    }

    public bool _hasTarget=false;
    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    public bool CanUseAbility = false;
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        touchDirection=GetComponent<TouchDirection>();
        animator=GetComponent<Animator>();
        playerPos = FindAnyObjectByType<PlayerController>().transform;
    }
    void Start()
    {
        
    }
    void Update()
    {
        HasTarget=attackZone.detectedColliders.Count > 0;
        CanUseAbility = !CanMove;
    }
    private void FixedUpdate()
    {
        walkDirectionVector = (Vector2)(playerPos.position - transform.position).normalized;
        if (touchDirection.IsGrounded)
        {
            
        }
        if (!CanMove)
        {
            //rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            walkDirectionVector = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
        else
        {
            transform.Translate(walkDirectionVector.x * walkSpeed * direction * Time.fixedDeltaTime, 0, 0);
        }
        //else
        //{
        //    //rb.velocity=new Vector2(Mathf.Lerp(rb.velocity.x,0,walkStopRate),rb.velocity.y);
        //}
    }
    public void FlipDirection()
    {
        if (playerPos.position.x > transform.position.x)
        {
            WalkDirection = WalkableDirection.Right;
            direction = -1;
        }
        else if (playerPos.position.x < transform.position.x)
        {
            WalkDirection = WalkableDirection.Left;
            direction = 1;
        }
        else
        {
            Debug.Log("Can't be Flip!");
        }
    }
}
