using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class FlyingEyes : MonoBehaviour
{
    //Idle Stage
    [Header("Idle")]
    [SerializeField] float idleMoveSpeed;
    [SerializeField] Vector2 idleMoveDirection;
    //Attack Up And Down
    [Header("AttackUpNDown")]
    [SerializeField] float attackMoveSpeed;
    [SerializeField] Vector2 attackMoveDirection;
    // Attack Player Stage
    [Header("AttackPlayer")]
    [SerializeField] float attackPlayerSpeed;
    private Transform player;
    private Vector2 playerPosition;
    private bool hasPlayerPosition;
    //Other
    [Header("Other")]
    [SerializeField] Transform groundCheckUp;
    [SerializeField] Transform groundCheckDown;
    [SerializeField] Transform groundCheckWall;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float delayJump;
    public GameObject Bite;
    public GameObject Body;
    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingWall;
    private bool goingUp=true;
    private bool facingLeft = true;
    private Rigidbody2D EnemyRB;
    private Animator enemyAnim;
    private bool isWaiting = false;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        Body.SetActive(true);
        Bite.SetActive(false);
        enemyAnim = GetComponent<Animator>();
        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();
        EnemyRB=GetComponent<Rigidbody2D>();
        player = GameObject.FindAnyObjectByType<PlayerController>().transform;
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(groundCheckWall.position, groundCheckRadius, groundLayer);
        if (spriteRenderer.flipX)
        {
            Bite.gameObject.transform.Rotate(0, 0, 180);
        }
        else
        {
            Bite.gameObject.transform.Rotate(0, 0, 0);
        }
    }
    void randomStateAttack()
    {
        int randomState = Random.Range(0, 2);
        if (randomState == 0)
        {
            enemyAnim.SetTrigger("AttackUpNDown");
        }
        else if (randomState == 1)
        {
            if (!goingUp)
            {
                ChangeDirection();
            }
            if (goingUp)
            {
                enemyAnim.SetTrigger("AttackPlayer");
            }
        }
    }
    private void FixedUpdate()
    {
        
    }
    void ChangeDirection()
    {
        goingUp = !goingUp;
        idleMoveDirection.y *= -1;
        attackMoveDirection.y *= -1;
    }
    void Flip()
    {
        facingLeft= !facingLeft;
        idleMoveDirection.x *= -1;
        attackMoveDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }
    //Trạng thái cơ bản của quái
    public void IdleStage()
    {
        Body.SetActive(true);
        Bite.SetActive(false);
        if (isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }
        EnemyRB.MovePosition(EnemyRB.position + idleMoveSpeed * idleMoveDirection* Time.fixedDeltaTime);
        if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
            }
            else
            {
                Flip();
            }
        }
    }
    void CantBehurt()
    {
        Bite.SetActive(false);
        Body.SetActive(false);
    }
    // Tấn công bằng cách di chuyển theo đường ziczac
    public void AttackUpNDown()
    {
        Body.SetActive(true);
        Bite.SetActive(false);
        if (!isWaiting)
        {
            if (isTouchingUp && goingUp || isTouchingDown && !goingUp)
            {
                StartCoroutine(WaitAndChangeDirection());
            }
            else
            {
                EnemyRB.MovePosition(EnemyRB.position + attackMoveSpeed * attackMoveDirection * Time.fixedDeltaTime);
            }
            if (isTouchingWall)
            {
                if (facingLeft)
                {
                    Flip();
                }
                else
                {
                    Flip();
                }
            }
        }
    }
    IEnumerator WaitAndChangeDirection()
    {
        isWaiting = true;
        yield return new WaitForSeconds(delayJump);
        ChangeDirection();
        isWaiting = false;
    }
    //Di chuyển lại gần Player và cắn Player
    public void AttackPlayer()
    {
        Body.SetActive(false);
        Bite.SetActive(true);
        if (isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }
        //Lấy hướng di chuyển là hướng tới Player
        if (!hasPlayerPosition)
        {
            hasPlayerPosition = true;
            playerPosition = player.position - transform.position;
            playerPosition.Normalize();
        }
        if (hasPlayerPosition)
        {
            EnemyRB.velocity = attackPlayerSpeed * playerPosition;
        }
        if (isTouchingWall || isTouchingDown)
        {
            EnemyRB.velocity = Vector2.zero;
            hasPlayerPosition = false;
            enemyAnim.SetTrigger("BitePlayer");
        }
        //EnemyRB.MovePosition(EnemyRB.position + attackPlayerSpeed * playerPosition * Time.deltaTime);
    }
    private void FlipTowardPlayer()
    {
        float playerDirection = player.position.x-transform.position.x;
        if (playerDirection > 0 && facingLeft)
        {
            Flip();
        }
        else if (playerDirection < 0 && !facingLeft)
        {
            Flip();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheckUp.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckDown.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckWall.position, groundCheckRadius);
    }
}
