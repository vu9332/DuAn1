using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    FireWormController fireWormController;
    private SpriteRenderer spriteFireBall;
    void Awake()
    {
        fireWormController=GameObject.FindAnyObjectByType<FireWormController>().GetComponent<FireWormController>();
        spriteFireBall = GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        if (fireWormController.facingLeft)
        {
            spriteFireBall.flipX = true;
        }
        else
        {
            spriteFireBall.flipX = false;
        }
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }
}
