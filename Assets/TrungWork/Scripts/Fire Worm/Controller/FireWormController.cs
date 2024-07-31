using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireWormController : MonoBehaviour
{
    [Header("FireWorm Controller")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceToAttackPlayer;
    [SerializeField] private Transform posLeft;
    [SerializeField] private Transform posRight;
    [Header("Sound Effect")]
    [SerializeField] private AudioClip soundSpray;
    private SpriteRenderer spriteFireWorm;
    private Rigidbody2D rb;
    private Vector2 directionMove;
    private Transform playerPosition;
    private Animator animFireWorm;
    private EnemyAIHealth enemyAIHealth;
    private bool canAttack=false;
    private Vector2 posTarget;
    private float dirLineCast;

    [Header("Please don't edit them!")]
    public float dirFireBall;
    public bool facingLeft = true;

    private void Awake()
    {
        animFireWorm= GetComponent<Animator>();
        spriteFireWorm=GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        enemyAIHealth= GetComponent<EnemyAIHealth>();
        playerPosition=GameObject.FindAnyObjectByType<PlayerController>().transform;
    }
    private void Start()
    {
        posTarget=posRight.position;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position,posRight.position) < .1f)
        {
            facingLeft = true;
            posTarget = posLeft.position;
            Flip();
            dirLineCast =-1;
        }
       if (Vector2.Distance(transform.position, posLeft.position) < .1f)
       {
            facingLeft = false;
            posTarget=posRight.position;
            dirLineCast = 1;
            Flip();
       }
        directionMove = posTarget - (Vector2)transform.position;
        directionMove.Normalize();
        if (canAttack)
        {
            animFireWorm.SetTrigger("Attack");
            directionMove = Vector2.zero;
        }
        if (enemyAIHealth.isDead)
        {
            directionMove = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        Vector2 endPos= transform.position + Vector3.right * distanceToAttackPlayer * dirLineCast;
        RaycastHit2D hit=Physics2D.Linecast(transform.position, endPos, 1 << LayerMask.NameToLayer("Player"));
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<PlayerController>())
            {
                canAttack=true;
                Debug.DrawLine(transform.position, endPos, Color.green);
            }
            else
            {
                canAttack = false;
                Debug.DrawLine(transform.position, endPos, Color.red);
            }
        }
        else
        {
            canAttack = false;
        }
        MoveToPlayer();
    }
    //Phun lửa
    [Header("Spray")]

    [SerializeField] private GameObject fireBallPrefabs;
    [SerializeField] private Transform posSprayFireBall;
    public void Spray()
    {
        GameObject fireBall = Instantiate(fireBallPrefabs, posSprayFireBall.position, Quaternion.identity);
        SoundFXManagement.Instance.PlaySoundFXClip(soundSpray, transform, 100);
    }
    //Di chuyển tới Player
    public void MoveToPlayer()
    {
        if(!enemyAIHealth.isHurting && rb.bodyType != RigidbodyType2D.Static)
        {
            rb.MovePosition(rb.position + moveSpeed * directionMove * Time.fixedDeltaTime);
        }
    }
    public void Flip()
    {
        if (facingLeft)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            dirFireBall = -1;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            dirFireBall = 1;
        }
    }
}
