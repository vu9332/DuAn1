using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSplash : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] protected float speed;
    protected float directionX;
    protected SpriteRenderer spriteRenderer;
    protected Transform boss;
    protected float damage;
    protected Collider2D coll;
    [SerializeField] protected ContactFilter2D contactFilter;
    [SerializeField] protected float groundDistance;
    protected RaycastHit2D[] wallHits=new RaycastHit2D[4];
    protected bool isTouchedWall;
    protected Vector2 directionTemp;

    protected virtual void Awake()
    {
        if (this.gameObject != null )
        {
            boss = FindAnyObjectByType<Demon>().GetComponent<Transform>();
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            coll= GetComponent<Collider2D>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected virtual void Start()
    {
        if(this.gameObject != null)
        {
            directionX = transform.position.x < boss.position.x ? -1 : 1;
        }
    }
    protected void TouchWall()
    {
        if (this.gameObject != null)
        {
            directionTemp = transform.position.x < boss.position.x ? Vector2.right : Vector2.left;
            isTouchedWall = coll.Cast(directionTemp,contactFilter,wallHits, groundDistance) > 0;
        }
    }
    protected void Move()
    {
        spriteRenderer.flipX = directionX == -1 ? false : true;
        rb.velocity = new Vector2(speed * directionX, rb.velocity.y);
        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            Destroy(gameObject);
        }
    }
}
