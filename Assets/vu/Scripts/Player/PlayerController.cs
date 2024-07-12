using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Move")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float moveWhileAttackSpeed;
    [SerializeField] private bool _isMoving = false;
    //moving
    public bool IsMoving { get { return _isMoving; } private set { _isMoving = value; myAnimator.SetBool(AnimationString.IsMoving, value); } }
    //running
    [SerializeField] private bool _isRunning = false;
    public bool IsRunning { get { return _isRunning; } private set { _isRunning = value; myAnimator.SetBool(AnimationString.IsRunning, value); } }


    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    private float jumpTimer;
    [SerializeField] private bool _isJumping = false;
    public bool IsJumping { get { return _isJumping; } private set { _isJumping = value; } }

    // dash
    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float maxDashDistance;
    Vector2 startDashPos;

    [SerializeField] private bool _isDash = false;
    public bool IsDash { get { return _isDash; } private set { _isDash = value; } }
    private bool CanDash = true;
    //roll
    [Header("Roll")]
    [SerializeField] private float rollSpeed;
    [SerializeField] private float maxRollDistance;
    Vector2 startRollPos;

    [SerializeField] private bool _isRolling = false;
    public bool IsRolling { get { return _isRolling; } private set { _isRolling = value; } }
    private bool CanRoll = true;



    [SerializeField] private float moveForceInAir;



    [Range(0f, 10)] public float rayCastMaxDistance;
    [SerializeField] private LayerMask enemyLayer;

    [Header(" ")]
    [SerializeField] private List<AudioClip> movingSoundEffect;

    [Header("WallSlide ")]
    [SerializeField] private bool _isWallSliding = false;
    public bool IsWallSliding { get { return _isWallSliding; } private set { _isWallSliding = value; myAnimator.SetBool(AnimationString.IsWalllSlide, value); } }
    [SerializeField] private float wallSlidingSpeed;

    [Header("WallJump ")]
    private bool isWallJumping;
    float wallJumpingDirection;
    float wallJumptime = 0.5f;
    float wallJumptimer;
    public Vector2 wallJumpPower = new Vector2(5f, 8f);

   [Header("Ledge ")]
   [ SerializeField] private float ledgeCheckDistance;
    private bool canClimbLedge=false;
    private bool ledgeDetected;

    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;
    [SerializeField] private Vector2 ledgeClimbOffset1;
    [SerializeField] private Vector2 ledgeClimbOffset2;
    
    public float currentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving &&!touchingDirection.IsOnWall&& !IsRolling)
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
            { //move while attack
                return (IsFacingRight) ? moveWhileAttackSpeed : moveWhileAttackSpeed * -1;
            }

        }
        set { }

    }


    Ghost ghost;
    CapsuleCollider2D capsuleCollider2D;
    PlayerCombat playerCombat;
    PlayerHealth playerHealth;
    TouchingDirection touchingDirection;
    Rigidbody2D rb;
    Animator myAnimator;
    ParticleSystem partic;
    Vector2 moveInput;


    // set direction
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
        Instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        playerCombat = GetComponent<PlayerCombat>();
        capsuleCollider2D = rb.GetComponent<CapsuleCollider2D>();
        partic = GameObject.FindAnyObjectByType<ParticleSystem>();
        playerHealth = GetComponent<PlayerHealth>();
        ghost = GetComponent<Ghost>();
    }

    void Update()
    {
        if (!isWallJumping)
        {
        ApplyMovement();
        }
        ApplyRolling();
        ApplyDashing();

        myAnimator.SetFloat(AnimationString.yVelocity, rb.velocity.y);

        Debug.DrawRay(this.transform.position, moveInput * rayCastMaxDistance, Color.yellow);

        ProcessWallSlide();
        ProcessWallJump();
        
    }
    #region Move
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
    private void ApplyMovement()
    {
       

            if (CanMove)
            {
                if (!IsRolling && !IsDash)
                    rb.velocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);
            }
           else if (!CanMove) moveInput = Vector2.zero;

    }
    #endregion
    #region Dash
    public void OnDash(InputAction.CallbackContext context)
    {

        if (CanPerformAction(context) && CanDash)
        {
            SoundFXManagement.Instance.PlaySoundFXClip(movingSoundEffect[0], this.transform, 1f);
            CanDash = false;
            IsDash = true;
            startDashPos = this.rb.position;
            ghost.makeGhost = true;
            // rb.velocity = Vector2.zero;
            myAnimator.SetTrigger(AnimationString.IsDashing);
            Vector2 direction = (IsFacingRight) ? Vector2.right : Vector2.left;
            rb.AddForce(new Vector2(dashSpeed * direction.x, 0), ForceMode2D.Impulse);
            StartCoroutine(EndDashRoutine());
            Debug.Log("Do Dash");
        }

    }
    private void ApplyDashing()
    {
        if (IsDash)
        {
            float dash = Vector2.Distance(startDashPos, this.transform.position);
            if (dash >= maxDashDistance)
            {
                rb.velocity = Vector2.zero;

            }

        }

        if (touchingDirection.IsOnWall) IsDash = false;
    }
    IEnumerator EndDashRoutine()
    {
        float dashTime = .25f;
        float dashCD = 5f;
        yield return new WaitForSeconds(dashTime);
        IsDash = false;
        ghost.makeGhost = false;
        //  trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        CanDash = true;
    }
    #endregion
    #region Roll
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (CanPerformAction(context) && touchingDirection.IsGround && CanRoll)
        {
            rb.velocity = Vector2.zero;
            CanRoll = false;
            IsRolling = true;
            startRollPos = rb.position;
            myAnimator.SetTrigger(AnimationString.IsRoll);
            Vector2 direction = (IsFacingRight) ? Vector2.right : Vector2.left;
            rb.gravityScale = (touchingDirection.IsGround) ? 1 : 0;
            rb.AddForce(new Vector2(rollSpeed * direction.x, 0), ForceMode2D.Impulse); ;
            StartCoroutine(EndRollRoutine());
            Debug.Log("Do");
            playerHealth.UseStamina(2);

        }

    }
    private void ApplyRolling()
    {
        if (IsRolling)
        {
            if (Vector2.Distance(startRollPos, this.transform.position) < maxRollDistance)
            {
                //  Enemy enemy;
                Vector2 direction = (IsFacingRight) ? Vector2.right : Vector2.left;
                RaycastHit2D hitsEnemy = Physics2D.Raycast(this.transform.position, direction, rayCastMaxDistance, enemyLayer);
                if (hitsEnemy)
                {
                    //hitsEnemy.collider.GetComponent<Enemy>();

                    Debug.Log("Trung");
                    // rb.gravityScale = 0;
                    capsuleCollider2D.isTrigger = true;
                    StartCoroutine(SetFalseTriggerCollier());

                }


            }
            else rb.velocity = Vector2.zero;
        }
    }

    IEnumerator EndRollRoutine()
    {
        float rollTime = .25f;
        float rollCD = .25f;
        //yield return new WaitForSeconds(rollTime);      
        yield return new WaitForSeconds(rollTime);
        IsRolling = false;
        yield return new WaitForSeconds(rollCD);
        CanRoll = true;

    }
    #endregion


    #region Jump,Slide and WallJump

    public void OnJump(InputAction.CallbackContext context)
    {
        if (touchingDirection.IsGround && CanJump(context))
        {
            SoundFXManagement.Instance.PlaySoundFXClip(movingSoundEffect[2], this.transform, .7f);
            myAnimator.SetTrigger(AnimationString.IsJumping);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }


        if (context.performed && wallJumptimer > 0)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumptimer = 0;

            if (transform.localScale.x != wallJumpingDirection)
            {
                IsFacingRight = !IsFacingRight;
            }

            Invoke(nameof(CancelWallJump), wallJumptime + 0.1f);
        }


    }

    private bool CanJump(InputAction.CallbackContext context)
    {
        return (context.performed && CanMove && touchingDirection.IsGround && !IsDash && !IsRolling);
    }

    private void ProcessWallSlide()
    {
        if (!touchingDirection.IsGround && touchingDirection.IsOnWall&&!canClimbLedge)
        {
            IsWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            IsWallSliding = false;
        }
    }

    private void ProcessWallJump()
    {
        if (IsWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumptimer = wallJumptime;
            CancelInvoke(nameof(CancelWallJump));
        }
        else if (wallJumptimer > 0f)
        {
            wallJumptimer -= Time.deltaTime;
        }
    }
    private void CancelWallJump()
    {
        isWallJumping = false;
    }
    #endregion 

  
    public void SetFacingDirection(Vector2 direction)
    {
        if (!isWallJumping)
        {

            if (direction.x > 0 && !IsFacingRight)
            {
                // right
                myAnimator.SetTrigger(AnimationString.IsTurnAround);
                CreateDust();
                IsFacingRight = true;
            }
            else if (direction.x < 0 && IsFacingRight)
            {
                //left
                CreateDust();
                myAnimator.SetTrigger(AnimationString.IsTurnAround);
                IsFacingRight = false;

            }
        }


    }

    IEnumerator SetFalseTriggerCollier()
    {
        yield return new WaitForSeconds(.25f);
        capsuleCollider2D.isTrigger = false;
        rb.gravityScale = 1;

    }

   
   
    public bool CanPerformAction(InputAction.CallbackContext context)
    {
        return context.started && CanMove && !IsRolling && !IsDash;
    }


    void CreateDust()
    {
        partic.Play();
    }
    public void PlayMoveSoundLeftFoot()
    {

        SoundFXManagement.Instance.PlaySoundFXClip(movingSoundEffect[0], this.transform, .25f);
    }
    public void PlayMoveSoundRightFoot()
    {

        SoundFXManagement.Instance.PlaySoundFXClip(movingSoundEffect[1], this.transform, .25f);
    }


}
