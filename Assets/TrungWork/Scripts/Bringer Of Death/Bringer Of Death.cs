using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath : MonoBehaviour
{
    [Header("Bringer Move:")]
    [SerializeField] private float speed;
    [SerializeField] private GameObject hitArea;
    private Rigidbody2D rb;
    private Animator animBringer;
    private Vector2 bringerDirection;
    private Transform playerPosition;
    private SpriteRenderer bringerSprite;
    private bool facingLeft = true;
    private float distance;
    private float dirSee;
    private bool canAttack=false;

    [Header("Bringer's Ability")]
    [SerializeField] private GameObject Spell;
    [SerializeField] private float spellOffset;
    private void Awake()
    {
        animBringer = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bringerSprite=GetComponent<SpriteRenderer>();
        playerPosition = GameObject.FindAnyObjectByType<PlayerController>().transform;
    }
    private void Start()
    {
        hitArea.SetActive(false);
    }
    private void Update()
    {
        distance = Vector2.Distance(transform.position, playerPosition.position);
        if (transform.position.x < playerPosition.position.x)
        {
            facingLeft = false;
            dirSee = 1;
        }
        else
        {
            facingLeft = true;
            dirSee = -1;
        }
        if (canAttack)
        {
            animBringer.SetTrigger("Attack");
            bringerDirection = Vector2.zero;
        }
        animBringer.SetFloat("distance",(float)distance);
    }
    [Header("Bringer WideSeen")]
    [SerializeField] private float distanceToAttackPlayer;
    private void FixedUpdate()
    {
        Vector2 endPos = transform.position + Vector3.right * distanceToAttackPlayer * dirSee;
        RaycastHit2D hit = Physics2D.Linecast(transform.position, endPos, 1 << LayerMask.NameToLayer("Player"));
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<PlayerController>())
            {
                canAttack = true;
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
    void DestroyBoss()
    {
        Destroy(gameObject);
    }
    public void MoveToPlayer()
    {
        hitArea.SetActive(false);
        bringerDirection= new Vector2(playerPosition.position.x-transform.position.x,rb.velocity.y);
        bringerDirection.Normalize();
        rb.MovePosition(rb.position + bringerDirection*speed*Time.fixedDeltaTime);
    }
    public void HandOfBlackness()
    {
        hitArea.SetActive(false);
        GameObject spell = Instantiate(Spell, new Vector2(playerPosition.position.x, transform.position.y+spellOffset), Quaternion.identity);
    }
    public void AttackPlayer()
    {
        hitArea.SetActive(true);
        if (!bringerSprite.flipX)
        {
            hitArea.transform.rotation=Quaternion.Euler(0,0,0);
        }
        else
        {
            hitArea.transform.rotation=Quaternion.Euler(0,-180,0);
        }
    }
    public void CantBeTakeDamage()
    {
        hitArea.SetActive(false);
    }
    public void Flip()
    {
        bringerSprite.flipX = !facingLeft;
    }
}
