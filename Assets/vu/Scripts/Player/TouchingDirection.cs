using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
   public static TouchingDirection Instance {  get;  set; }  
   
    public ContactFilter2D castFilter; 
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    public float groundDistance = 0.05f;
    public float wallDistance = 0.05f;
    Vector2 wallDirection=>(playerController.IsFacingRight)?Vector2.right:Vector2.left;

    CapsuleCollider2D touchingColl;
    Animator myAnimator;
    PlayerController playerController;

    [Header("GroundCheck")]
    [SerializeField]
    private bool _isGround = true;
    public bool IsGround { get { return _isGround; } private set { _isGround = value; myAnimator.SetBool(AnimationString.IsGrounded, value); } }

    [Header("WallCheck")]
    [SerializeField]
    private bool _isOnWall = true;
    public bool IsOnWall { get { return _isOnWall; } private set { _isOnWall = value; myAnimator.SetBool(AnimationString.IsOnWall, value); } }  
    // wall
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Vector2 wallCheckSize;

    [Header("OnWallNoSlide")]
    [SerializeField]
    private bool _isOnWallNoSlide = false;
    public bool IsOnWallNoSlide { get { return _isOnWallNoSlide; } set { _isOnWallNoSlide = value;myAnimator.SetBool(AnimationString.IsLedgeClimb, value); } }

    //[SerializeField] private Transform ledgeCheck;
    //[SerializeField] private float ledgeCheckDistance;
    //[SerializeField] private LayerMask ledgeLayerMask;
    private void Awake()
    {
       Instance = this;
    }
    void Start()
    {
        // rb=GetComponent<Rigidbody2D>();
        touchingColl = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
        playerController=GetComponent<PlayerController>();  
    }

    // Update is called once per frame
    void Update()
    {

        //IsGround = IsGrounded();
       IsOnWall = IsWalled();
    }
 
    private void FixedUpdate()
    {
        IsGround = touchingColl.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWallNoSlide = touchingColl.Cast(wallDirection, castFilter, wallHits, groundDistance) > 0;
       
    }
    //public bool IsLedge()
    //{
    //   // return Physics2D.Raycast(ledgeCheck.position, Vector2.right, ledgeCheckDistance, ledgeLayerMask);
    //}
    public bool IsWalled()
    {
        return Physics2D.OverlapBox(wallCheck.position, wallCheckSize,0,wallLayer);
    }
    void OnDrawGizmosSelected()
    {
        //Gizmos.DrawSphere(wallCheck.position, wallCheckRadius);

        Gizmos.color = Color.blue;
      

    }
}
