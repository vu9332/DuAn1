using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rollSpeed;
    [SerializeField] private float moveWhileAttackSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] public bool canReceiveInput;
    [SerializeField] public bool inputReceive;




    public float currentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirection.IsOnWall && !IsRolling)
                {
                    if (IsRunning)
                    { //running
                        return runSpeed;
                    }
                    else
                    {
                        //walk
                        return walkSpeed;

                    }
                }              
                else
                {
                    //idle
                    return 0;
                }
            }

            else
            {
                //attackMove
                //if (IsFacingRight)
                //{
                //    return moveWhileAttackSpeed;
                //}
                //else
                //{
                //    return moveWhileAttackSpeed * -1;
                //}
                
               return (IsFacingRight) ? moveWhileAttackSpeed: moveWhileAttackSpeed * -1;
            }

        }
       // private set { }

    }

    //private bool _isNextAttackType;
    // public bool IsNextAttackType;
    CapsuleCollider2D capsuleCollider2D;
    PlayerCombat playerCombat;
    TouchingDirection touchingDirection;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator myAnimator;
    Vector2 moveInput;


    [SerializeField] private bool _isMoving = false;
    public bool IsMoving { get { return _isMoving; } private set { _isMoving = value; myAnimator.SetBool(AnimationString.IsMoving, value); } }


    [SerializeField] private bool _isRunning = false;
    public bool IsRunning { get { return _isRunning; } private set { _isRunning = value; myAnimator.SetBool(AnimationString.IsRunning, value); } }



    [SerializeField] private bool _isRolling = false;
    public bool IsRolling { get { return _isRolling; } private set { _isRolling = value; } }
    private bool CanRoll = true;


    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove { get { return myAnimator.GetBool(AnimationString.canMove); } }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        playerCombat = GetComponent<PlayerCombat>();
        capsuleCollider2D = rb.GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //rb.MovePosition(new Vector2(moveInput.x*speed*Time.fixedDeltaTime, moveInput.y*jumpHight));
        if (CanMove)
        {
            
            //if (IsRolling)
            //{
            //     moveInput=Vector2.zero;
            //    //rb.velocity = Vector2.zero;
            //}
            if(CanRoll)
              rb.velocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);
        }
        else
        {
            if (!playerCombat.CanAttack )
            {
                // moveInput=Vector2.zero;
                rb.velocity = Vector2.zero;

            }          
                rb.AddForce(new Vector2(currentMoveSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);

        }
        myAnimator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
      
        if (CanMove)
        { 
                moveInput = context.ReadValue<Vector2>();
                IsMoving = moveInput != Vector2.zero;
                SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false; 
            moveInput = Vector2.zero;
        }
        // if (!IsRolling)


    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirection.IsGround && CanMove&&!IsRolling)
        {

            myAnimator.SetTrigger(AnimationString.IsJumping);
            rb.AddForce(Vector2.up *jumpForce, ForceMode2D.Impulse);

        }
    }
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirection.IsGround && CanMove)
        {
            if (!IsRolling && CanRoll)
            {
                CanRoll = false;
                IsRolling = true;
                myAnimator.SetTrigger(AnimationString.IsRoll);              
                StartCoroutine(EndRollRoutine());
                Debug.Log("Do");
            }

        }

    }
    IEnumerator EndRollRoutine()
    {
         int direction = (IsFacingRight) ? 1 : -1;
        rb.AddForce(new Vector2(rollSpeed*direction*Time.deltaTime, 0), ForceMode2D.Impulse); ;
        float rollTime = .25f;
        float rollCD = .25f;
        //yield return new WaitForSeconds(rollTime);
        int gravity = (touchingDirection.IsGround) ? 1 : 0;
        rb.gravityScale = gravity;
        capsuleCollider2D.isTrigger = true;
        yield return new WaitForSeconds(rollTime);
        capsuleCollider2D.isTrigger = false;
        rb.gravityScale = 1;
        yield return new WaitForSeconds(rollTime);
        IsRolling = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(rollCD);
        CanRoll = true;

    }
    public void InputManager()
    {
        if (!canReceiveInput)
            canReceiveInput = true;
        else
            canReceiveInput = false;
    }
    void SetFacingDirection(Vector2 direction)
    {
        if (direction.x > 0 && !IsFacingRight)
        {
            // right
            IsFacingRight = true;
        }
        else if (direction.x < 0 && IsFacingRight)
        {
            //left
            IsFacingRight = false;
        }
    }
   
}
