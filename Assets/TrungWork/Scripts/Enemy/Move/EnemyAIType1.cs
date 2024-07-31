using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAIType1 : MonoBehaviour
{
    [SerializeField] private Transform posLeft;
    [SerializeField] private Transform posRight;
    [SerializeField] private float speed;
    [SerializeField] private bool facingLeft = false;
    private Vector2 posTarget;
    private Rigidbody2D rb;
    private EnemyAIFindPlayer enemyAIFindPlayer;
    private EnemyAIHealth enemyAIHealth;
    public int dirLineCast { get; set; }
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        enemyAIFindPlayer = GetComponent<EnemyAIFindPlayer>();
        enemyAIHealth = GetComponent<EnemyAIHealth>();
    }
    private void Start()
    {
        posTarget = Vector2.left;
        dirLineCast = 1;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, posLeft.position) <.1f || transform.position.x < posLeft.position.x)
        {
            //posTarget=posRight.position;
            posTarget = Vector2.right;
            facingLeft = false;
            Flip();
        }
        if (Vector2.Distance(transform.position, posRight.position) < .1f || transform.position.x > posRight.position.x)
        {
            //posTarget = posLeft.position;
            posTarget = Vector2.left;
            facingLeft = true;
            Flip();
        }
    }
    private void FixedUpdate()
    {
        if (!enemyAIFindPlayer.canAttack && !enemyAIHealth.isHurting)
        {
            rb.velocity=new Vector2(speed * posTarget.x,rb.velocity.y);
            //rb.MovePosition(rb.position + (posTarget - (Vector2)transform.position).normalized * speed * Time.fixedDeltaTime);
        }
    }
    private void Flip()
    {
        if (facingLeft)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            dirLineCast = -1;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,180,0);
            dirLineCast = 1;
        }
    }
}
