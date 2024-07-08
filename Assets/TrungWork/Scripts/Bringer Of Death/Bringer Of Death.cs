using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath : MonoBehaviour
{
    [Header("Player:")]
    [SerializeField] private float speed;
    [SerializeField] private GameObject hitArea;
    private Rigidbody2D rb;
    private Animator animBringer;
    private Vector2 bringerDirection;
    private Transform playerPosition;
    private SpriteRenderer bringerSprite;
    private bool facingLeft = true;
    public bool foundPlayer = false;
    private float distance;

    [Header("Bringer's Ability")]
    [SerializeField] private GameObject Spell;
    [SerializeField] private float spellOffset;
    private void Awake()
    {
        animBringer = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bringerSprite=GetComponent<SpriteRenderer>();
        playerPosition = GameObject.FindAnyObjectByType<Player>().transform;
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
        }
        else
        {
            facingLeft = true;
        }
        animBringer.SetFloat("distance",(float)distance);
    }
    private void FixedUpdate()
    {
        if (distance <= 4)
        {
            foundPlayer = true;
        }
        else
        {
            foundPlayer = false;
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
        Destroy(spell,1.2f);
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
    public void Flip()
    {
        bringerSprite.flipX = !facingLeft;
    }
}
