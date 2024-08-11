using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack1 : MonoBehaviour
{
    [SerializeField] private float distance;
    private SpriteRenderer sp;
    private bool isAttacking=false;
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyAIFindPlayer EnemyAIFindPlayer;
    private void Awake()
    {
        sp=GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        EnemyAIFindPlayer=GetComponent<EnemyAIFindPlayer>();
    }
    void Start()
    {
        
    }
    private void Update()
    {
        if (EnemyAIFindPlayer.canAttack)
        {
            if (transform.position.x < PlayerController.Instance.transform.position.x)
            {
                sp.flipX = false;
            }
            else
            {
                sp.flipX = true;
            }
        }
        anim.SetBool("Attack", EnemyAIFindPlayer.canAttack);
    }
}
