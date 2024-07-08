using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

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




    [Range(0f, 10)] public float rayCastMaxDistance;
    [SerializeField] private LayerMask enemyLayer;

    [Header(" ")]
    [SerializeField] private List<AudioClip> movingSoundEffect;


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
       ghost=GetComponent<Ghost>();


    }

  
    void Update()
    {
       
        if (CanMove)
        {
            if (!IsRolling && !IsDash)
                rb.velocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);
          

        }
        else if (!CanMove) moveInput = Vector2.zero;


        myAnimator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
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
        Debug.DrawRay(this.transform.position, moveInput * rayCastMaxDistance, Color.yellow);
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
        if (context.started && CanMove)
        {
            if (touchingDirection.IsGround && (!IsRolling && !IsDash))
            {
                SoundFXManagement.Instance.PlaySoundFXClip(movingSoundEffect[2], this.transform, .7f);
                myAnimator.SetTrigger(AnimationString.IsJumping);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirection.IsGround && CanMove)
        {
            rb.velocity = Vector2.zero;
            if (!IsRolling && CanRoll && !IsDash)
            {
              
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

    }
    public void OnDash(InputAction.CallbackContext context)
    {

        if (context.started && CanMove)
        {
            if ((!IsDash && CanDash) && !IsRolling)
            {
                SoundFXManagement.Instance.PlaySoundFXClip(movingSoundEffect[0], this.transform, 1f);
                CanDash = false;
                IsDash = true;
                startDashPos = this.rb.position;
                ghost.makeGhost = true;
                rb.velocity = Vector2.zero;
                myAnimator.SetTrigger(AnimationString.IsDashing);
                Vector2 direction = (IsFacingRight) ? Vector2.right : Vector2.left;
                rb.AddForce(new Vector2(dashSpeed * direction.x, 0), ForceMode2D.Impulse);
                //
                StartCoroutine(EndDashRoutine());
                Debug.Log("Do Dash");
            }

        }

    }
    public void SetFacingDirection(Vector2 direction)
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
    IEnumerator SetFalseTriggerCollier()
    {
        yield return new WaitForSeconds(.25f);
       
        capsuleCollider2D.isTrigger = false;
          rb.gravityScale = 1;

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
   
   
    void CreateDust()
    {
        partic.Play();
    }
    public void PlayMoveSoundLeftFoot()
    {
       
            SoundFXManagement.Instance.PlaySoundFXClip(movingSoundEffect[0], this.transform, .25f);
    }public void PlayMoveSoundRightFoot()
    {
     
            SoundFXManagement.Instance.PlaySoundFXClip(movingSoundEffect[1], this.transform, .25f);
    }
   
    
}
