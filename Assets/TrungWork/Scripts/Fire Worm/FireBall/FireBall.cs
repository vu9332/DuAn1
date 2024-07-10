using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject ExplosionFireBall;
    private Rigidbody2D rb;
    private FireWormController fireWormController;
    private SpriteRenderer spriteFireBall;
    private Animator animFireWorm;
    void Awake()
    {
        fireWormController=GameObject.FindAnyObjectByType<FireWormController>().GetComponent<FireWormController>();
        spriteFireBall = GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (fireWormController.dirFireBall == -1)
        {
            spriteFireBall.flipX = true;
        }
        else
        {
            spriteFireBall.flipX = false;
        }
        rb.AddForce(transform.right * speed * fireWormController.dirFireBall, ForceMode2D.Impulse);
    }
    private void Update()
    {
        Destroy(gameObject, 3f);
    }

    //Nếu va chạm với Player thì sinh ra hiệu ứng nổ
    private void OnTriggerEnter2D(Collider2D player)
    {
        if(player.gameObject.GetComponent<PlayerHealth>() != null)
        {
            Instantiate(ExplosionFireBall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
