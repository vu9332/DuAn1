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
    public static PlayerController Instance {  get; set; }

    [SerializeField] public PlayerData playerData;

    [Header("Move")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float moveWhileAttackSpeed;

  
    public bool IsMoving { get { return playerData._isMoving; } private set { playerData._isMoving = value; myAnimator.SetBool(AnimationString.IsMoving, value); } }
    //running
    
    public bool IsRunning { get { return playerData._isRunning; } private set { playerData._isRunning = value; myAnimator.SetBool(AnimationString.IsRunning, value); } }


    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    private float jumpTimer;
 
    public bool IsJumping { get { return playerData._isJumping; } private set { playerData._isJumping = value; } }

    // dash
    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float maxDashDistance;
    Vector2 startDashPos;
    public bool CanDash = true;
    public bool IsDash { get { return playerData. _isDash; } private set { playerData. _isDash = value; } }
   
    //roll
    [Header("Roll")]
    [SerializeField] private float rollSpeed;
    [SerializeField] private float maxRollDistance;
    [SerializeField] float rollCD = .25f;
    Vector2 startRollPos;
  
    public bool IsRolling { get { return playerData._isRolling; } private set { playerData._isRolling = value; } }
    private bool CanRoll = true;
    [Range(0f, 10)] public float rayCastMaxDistance;
    [SerializeField] private LayerMask enemyLayer;
    [Header(" ")]
    [SerializeField] private List<AudioClip> movingSoundEffect;

    [Header("Wallslide")]
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] public bool IsWallSliding { get { return playerData._isWallSliding; } private set { playerData._isWallSliding = value; myAnimator.SetBool(AnimationString.IsWalllSlide, value); } }

    [Header("WallJump ")]
    private bool isWallJumping;
    float wallJumpingDirection;
    float wallJumptime = 0.5f;
    float wallJumptimer;
    public Vector2 wallJumpPower = new Vector2(5f, 8f);

    private bool PassiveSkillsBoardOpen = false;
      
    
    public float currentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving &&!touchingDirection.IsOnWallNoSlide&& !IsRolling)
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
  public  Animator myAnimator;
    ParticleSystem partic;
  public  Vector2 moveInput;


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
                CreateDust();
                myAnimator.SetTrigger(AnimationString.IsTurnAround);
            }

            _isFacingRight = value;

        }
    }

    public bool CanMove { get { return myAnimator.GetBool(AnimationString.canMove); } }

    [Header("Camera Stuff")]
    [SerializeField] private GameObject _cameraFollowGo; 
    private CameraFollowObject _cameraFollowObjet;
    //private float _fallSpeedYDamingChangeThreshold;
    private void Awake()
    {
        if(Instance==null)
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
        _cameraFollowObjet=_cameraFollowGo.GetComponent<CameraFollowObject>();
      ///_fallSpeedYDamingChangeThreshold=CameraManager.instance.fallSpeedYDampingChangeThreshold;
    }

    void Update()
    {
        if (!isWallJumping&&!PlayerHealth.Instance.iSDeath)
        {
        ApplyMovement();
          
        }
        ApplyRolling();
        ApplyDashing();
        myAnimator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
        ProcessWallSlide();
        ProcessWallJump();

        //
        //if (rb.velocity.y<=-_fallSpeedYDamingChangeThreshold&&!CameraManager.instance.IsLerpingYDamping&&!CameraManager.instance.LerpededFromPlayerFalling)
        //{
        //    CameraManager.instance.LerpYDamping(true);        
        //}
        //if (rb.velocity.y>=-_fallSpeedYDamingChangeThreshold&&!CameraManager.instance.IsLerpingYDamping&&CameraManager.instance.LerpededFromPlayerFalling)
        //{
        //    CameraManager.instance.LerpededFromPlayerFalling=false;
        //    CameraManager.instance.LerpYDamping(false);        
        //}
        //

        
    }
    #region Move
    public void OnMove(InputAction.CallbackContext context)
    {

        if (CanMove)
        {
            moveInput = context.ReadValue<Vector2>();
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
            Debug.Log("Move");
        }
        else
        {
            IsMoving = false;
           // moveInput = Vector2.zero;
          //  Debug.Log("NotMove");
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
          

        if (CanPerformAction(context) &&CanDash)
        {
            ActiveDash();
        }

    }
    public void ActiveDash()
    {
        SoundFXManagement.Instance.PlaySoundFXClip(movingSoundEffect[0], this.transform, 1f);
        CanDash = false;
        IsDash = true;
        startDashPos = this.rb.position;
        ghost.makeGhost = true;
        myAnimator.SetTrigger(AnimationString.IsDashing);
        Vector2 direction = (IsFacingRight) ? Vector2.right : Vector2.left;
        rb.AddForce(new Vector2(dashSpeed * direction.x, 0), ForceMode2D.Impulse);
        StartCoroutine(EndDashRoutine());
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
       
       Player_Abilities.dashAbilityCoolDown();
        float dashTime = .25f;        
        yield return new WaitForSeconds(dashTime);
        IsDash = false;
        ghost.makeGhost = false;
        //  trailRenderer.emitting = false;
        yield return new WaitForSeconds(playerData.dashCD);
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
            playerHealth.UseStamina(2);

        }

    }
    private void ApplyRolling()
    {
        if (IsRolling)
        {
            if (Vector2.Distance(startRollPos, this.transform.position) < maxRollDistance)
            {
                Vector2 direction = (IsFacingRight) ? Vector2.right : Vector2.left;
                RaycastHit2D hitsEnemy = Physics2D.Raycast(this.transform.position, direction, rayCastMaxDistance, enemyLayer);
                if (hitsEnemy)
                {                 
                    rb.gravityScale = 0;
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
            Debug.Log("DoJump");
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
        if (!touchingDirection.IsGround && touchingDirection.IsOnWall)
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
               
                IsFacingRight = true;
                _cameraFollowObjet.CallTurn();
            }
            else if (direction.x < 0 && IsFacingRight)
            {
                //Left
                IsFacingRight = false;
               _cameraFollowObjet.CallTurn();

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

    public void OpenPassiveSkillsBoard(InputAction.CallbackContext context)
    {
        if(context.started)
        {          
            if (!PassiveSkillsBoardOpen)
            {
                StatusManagement.PressShowBoard();
                PassiveSkillsBoardOpen = true;
                
            }
            else if (PassiveSkillsBoardOpen)
            {
                StatusManagement.PressHideBoard();
                PassiveSkillsBoardOpen = false;
            }
        }
    }
    
}
