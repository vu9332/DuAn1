using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{

    // Start is called before the first frame update
    // Rigidbody2D rb;
    public ContactFilter2D castFilter;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] cellHits = new RaycastHit2D[5];
    public float groundDistance = 0.05f;
    public float wallDistance = 0.5f;
    public float cellDistance = 0.05f;

    CapsuleCollider2D touchingColl;
    Animator myAnimator;

    [SerializeField]    
    private bool _isGround=true;
    public bool IsGround { get { return _isGround; } private set { _isGround = value; myAnimator.SetBool(AnimationString.IsGrounded, value); } }


    [SerializeField]
    private bool _isOnWall = true;
    public bool IsOnWall { get { return _isOnWall; } private set { _isOnWall = value; myAnimator.SetBool(AnimationString.IsOnWall, value); } }
    Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;


    [SerializeField]
    private bool _isOnCelling = true;
    public bool IsOnCelling { get { return _isOnCelling; } private set { _isOnCelling = value; myAnimator.SetBool(AnimationString.IsOnCelling, value); } }

    void Start()
    {
        // rb=GetComponent<Rigidbody2D>();
        touchingColl = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(IsOnWall);
    }
    private void FixedUpdate()
    {
        IsGround = touchingColl.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingColl.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCelling = touchingColl.Cast(Vector2.up, castFilter, cellHits, cellDistance) > 0;

    }
}
