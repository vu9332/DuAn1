using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDirection : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float cellingDistance = 0.05f;
    BoxCollider2D touchingCol;
    RaycastHit2D[] groundHits=new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] cellingHits = new RaycastHit2D[5];
    Animator animator;
    [SerializeField] private bool _isGrounded = true;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }
    [SerializeField] private bool _isOnWall = true;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }
    [SerializeField] private bool _isOnCelling = true;
    public bool IsOnCelling
    {
        get
        {
            return _isOnCelling;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isOnCelling, value);
        }
    }

    private Vector2 walkCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right: Vector2.left;
    private void Awake()
    {
        touchingCol = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(walkCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCelling = touchingCol.Cast(Vector2.up, castFilter, cellingHits, cellingDistance) > 0;
    }
}
