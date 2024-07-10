using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWormController : MonoBehaviour
{
    [Header("FireWorm Controller")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceToAttackPlayer;
    private SpriteRenderer spriteFireWorm;
    private Rigidbody2D rb;
    private Vector2 directionMove;
    private Transform playerPosition;
    private Animator animFireWorm;
    private bool canAttack=false;

    [Header("Please don't edit them!")]
    public float dirFireBall;
    public bool facingLeft = false;
    public Vector2 dirPlayer;
    private void Awake()
    {
        animFireWorm= GetComponent<Animator>();
        spriteFireWorm=GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerPosition=GameObject.FindAnyObjectByType<PlayerController>().transform;
    }
    private void Update()
    {
        directionMove=playerPosition.position-transform.position;
        directionMove.Normalize();
        if(transform.position.x > playerPosition.position.x)
        {
            facingLeft = true;
        }
        else
        {
            facingLeft = false;
        }
        if(canAttack)
        {
            animFireWorm.SetTrigger("Attack");
            directionMove = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        Vector2 endPos= transform.position + Vector3.right * distanceToAttackPlayer * dirFireBall;
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
    }
    
    //Phun lửa
    [Header("Spray")]

    [SerializeField] private GameObject fireBallPrefabs;
    [SerializeField] private Transform posSprayFireBall;
    public void Spray()
    {
        GameObject fireBall = Instantiate(fireBallPrefabs, posSprayFireBall.position, Quaternion.identity);
    }
    //Di chuyển tới Player
    public void MoveToPlayer()
    {
        rb.MovePosition(rb.position + moveSpeed * directionMove * Time.fixedDeltaTime); 
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
