using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWormController : MonoBehaviour
{
    [Header("Fire Worm")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject fireBallPrefabs;
    [SerializeField] private Transform posSprayFireBall;
    private SpriteRenderer spriteFireWorm;
    private Rigidbody2D rb;
    private Vector2 directionMove;
    private Transform playerPosition;
    private Animator animFireWorm;
    private float distance;

    public bool facingLeft = false;
    public Vector2 dirPlayer;
    private void Awake()
    {
        animFireWorm= GetComponent<Animator>();
        spriteFireWorm=GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerPosition=GameObject.FindAnyObjectByType<Player>().transform;
    }
    private void Update()
    {
        directionMove=playerPosition.position-transform.position;
        distance = Vector2.Distance(playerPosition.position,transform.position);
        directionMove.Normalize();
        if(transform.position.x > playerPosition.position.x)
        {
            facingLeft = true;
        }
        else
        {
            facingLeft = false;
        }
        animFireWorm.SetFloat("distance",(float)distance);
    }
    private void FixedUpdate()
    {
        
    }
    public void Spray()
    {
        GameObject fireBall = Instantiate(fireBallPrefabs, posSprayFireBall.position, Quaternion.identity);
    }
    public void DeleteSpray()
    {
        Destroy(gameObject);
    }
    public void MoveToPlayer()
    {
        rb.MovePosition(rb.position + moveSpeed * directionMove * Time.fixedDeltaTime);
    }
    public void Flip()
    {
        spriteFireWorm.flipX = facingLeft;
    }
}
