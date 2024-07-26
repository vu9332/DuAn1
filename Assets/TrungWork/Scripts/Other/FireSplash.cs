using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSplash : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed;
    private float directionX => transform.position.x < boss.position.x && boss!=null ? -1:1;
    private SpriteRenderer spriteRenderer;
    private Transform boss;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (gameObject != null )
        {
            boss = FindAnyObjectByType<Demon>().GetComponent<Transform>();
        }
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        spriteRenderer.flipX = directionX ==-1 ?false : true;
        rb.velocity=new Vector2(speed * directionX,rb.velocity.y);
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
