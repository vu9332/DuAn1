using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [Range(0f, 10f)]
    public float attackRange;
    [SerializeField] private LayerMask enemyLayer;

    [Header("NormaAttack (Left Mouse) ")]
    [SerializeField] private float playerDamage;
    [SerializeField] private bool trigger;
    [SerializeField] private float combo;
    [SerializeField] private float comboTiming = .5f;
    [SerializeField] private float comboTempo;
    [SerializeField] private float attackNormalCoolDown;
    [Range(0f, 10f)]
    public float maxDistanceWhileAttack;
    Vector2 startMoveWhileAttackPos;
    

    int comboNumber = 3;
    // Nomarl
    [SerializeField]
    private bool _normalAttack;
    public bool IsNormalAttack { get { return _normalAttack; } set { _normalAttack = value; } }

    //[SerializeField]
    //private bool _normalAttack2 = true;
    //public bool IsNormalAttack2 { get { return _normalAttack2; } set { _normalAttack2 = value; } }


    //
    [Header("Skill 1 ( F )")]
    [SerializeField] private float attackComboCoolDown;
    [Range(0f, 10f)]
    public float maxDistanceSkillOneSlide;
    Vector2 startPointSlideSkillOne;
    //skill one
    [SerializeField]
    private bool _isSkillOne = true;
    public bool IsSkillOne { get { return _isSkillOne; } set { _isSkillOne = value; } }


    //
    [Header("Skill 2 ( Q )")]
    [SerializeField] private Transform slashPoint;
    [SerializeField] private GameObject slashPrefabs;
    private Vector3 slashToEnemy;
    [SerializeField] private float slashCoolDown;
    [Range(0,100)] public float slashAttackRange;
    [SerializeField]
    private bool _isSkillTwo = true;
    public bool IsSkillTwo { get { return _isSkillTwo; } set { _isSkillTwo = value; } }


    [Header("Skill buff ( r )")]

    [SerializeField] private Transform buffAttackPoint;
    [SerializeField] private GameObject buffAttackPrefabs;





    [SerializeField]
    private bool _isBuff = false;
    public bool IsBuff { get { return _isBuff; } set { _isBuff = value; } }

    [SerializeField]
    private bool _canAttack = true;
    public bool CanAttack { get { return _canAttack; } private set { _canAttack = value; } }


    TouchingDirection touchingDirection;
    Animator myAnimator;
    Rigidbody2D rb;


    public bool CanMove { get { return myAnimator.GetBool(AnimationString.canMove); } }

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        rb = GetComponent<Rigidbody2D>();
        comboTempo = comboTiming;
        //combo = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanMove)
        {
            if (!CanAttack)
            {

                if (IsNormalAttack)
                {

                    if (Vector2.Distance(startMoveWhileAttackPos, this.transform.position) <= maxDistanceWhileAttack)
                    {
                        rb.AddForce(new Vector2(PlayerController.instance.currentMoveSpeed, 0), ForceMode2D.Impulse);
                        Debug.Log("Normal");
                    }
                    else rb.velocity = Vector2.zero;

                }
                else if ( !IsSkillOne&&!IsNormalAttack)
                {
                    if (Vector2.Distance(startPointSlideSkillOne, this.transform.position) <= maxDistanceSkillOneSlide)
                    {
                        Debug.Log("2");
                        rb.AddForce(new Vector2(PlayerController.instance.currentMoveSpeed, 0), ForceMode2D.Impulse);
                    }
                    else rb.velocity = Vector2.zero;
                }
            }


        }

        comboTempo -= Time.deltaTime;
        if (comboTempo < 0)
        {
            combo = 1;
        }
    }
  
    public void OnAttack(InputAction.CallbackContext context)
    {
       if((!PlayerController.instance.IsRolling&&!PlayerController.instance.IsDash)&&touchingDirection.IsGround)
        {
            rb.velocity = Vector2.zero;
            if (context.started && comboTempo < 0)
            {
                
                IsNormalAttack = true;
                startMoveWhileAttackPos = rb.position;
                StartCoroutine(NormalAttackCoolDown(attackNormalCoolDown));
                myAnimator.SetTrigger(AnimationString.IsNormalAttack + combo);
                comboTempo = comboTiming;
                if (IsBuff)
                {
                    BuffAttack();
                }
                    
               
                // 
                //if (CanAttack && (IsNormalAttack && !IsNormalAttack2))
                //{
                //    CanAttack = false;
                //    IsNormalAttack = false;
                //    IsNormalAttack2 = true;
                //    myAnimator.SetTrigger(AnimationString.IsNormalAttack);
                //    StartCoroutine(NormalAttackCoolDown( attackNormalCoolDown));



                //}
                //else if (CanAttack && (!IsNormalAttack && IsNormalAttack2))
                //{
                //    IsNormalAttack = true;
                //    IsNormalAttack2 = false;
                //    CanAttack = false;
                //    //CanAttack = false;
                //    myAnimator.SetTrigger(AnimationString.IsNormalAttack);
                //    myAnimator.SetTrigger(AnimationString.IsNormalAttack2);
                //    StartCoroutine(NormalAttackCoolDown( attackNormalCoolDown));


                //}
                //combo++;

            }
            else if (context.started && (comboTempo > 0 && comboTempo <= 1f))
            {
                IsNormalAttack = true;
                combo++;
                if (combo > comboNumber)
                {
                    combo = 1;
                }
                startMoveWhileAttackPos = rb.position;
                //CanAttack = false;
                myAnimator.SetTrigger(AnimationString.IsNormalAttack + combo);
                if (IsBuff) { BuffAttack(); }
                StartCoroutine(NormalAttackCoolDown(attackNormalCoolDown));
                comboTempo = comboTiming;

            }
            else if (comboTempo < 0 && context.canceled)
            {
                IsNormalAttack = false;
            }

        }
       else if(!touchingDirection.IsGround && context.started)
        {
            myAnimator.SetTrigger(AnimationString.IsAirAttack);
        }

    }
    IEnumerator NormalAttackCoolDown(float coolDownTime)
    {
       
        CanAttack = false;
        yield return new WaitForSeconds(coolDownTime);
        yield return null;
        CanAttack = true;
    }


    public void OnSkillOne(InputAction.CallbackContext context)
    {

        if (context.started && touchingDirection.IsGround && !PlayerController.instance.IsRolling)
        {
            rb.velocity = Vector2.zero;
            if (IsSkillOne && CanAttack)
            {

               startPointSlideSkillOne=rb.position;
                CanAttack = false;
                IsSkillOne = false;
                IsNormalAttack = false;
                myAnimator.SetTrigger(AnimationString.IsSkillOne        );
                StartCoroutine(SkillOneCoolDown(attackComboCoolDown));             
               
               
            }
            
        }   
       

    }
    IEnumerator SkillOneCoolDown(float coolDownTime)
    {
         yield return new WaitForSeconds(1f);
        CanAttack = true;
        IsNormalAttack=true;
        yield return new WaitForSeconds(coolDownTime);
        
        IsSkillOne =true;
    }



    public void OnSkillTwo(InputAction.CallbackContext context)
    {
        if (context.started&&(!PlayerController.instance.IsRolling&&!PlayerController.instance.IsDash))
        {
            if (IsSkillTwo&&CanAttack)
            {
                IsSkillTwo = false;
                CanAttack=false;
                myAnimator.SetTrigger(AnimationString.IsSkillTwo);
                FindingEnemy();
                StartCoroutine(SkillTwoCoolDown(slashCoolDown));
               

            }
           
        }
        CanAttack=true;
    }
    IEnumerator SkillTwoCoolDown(float coolDownTime)
    {
        yield return new WaitForSeconds(coolDownTime);
        IsSkillTwo = true;
    }
    public void Slash()
    {
        GameObject slash = Instantiate(slashPrefabs, slashPoint.position, transform.rotation);
        Slash slashScript = slash.GetComponent<Slash>();
        if (slash != null)
        {           
            slashScript.SetSlashDirection(slashToEnemy);
            //float angle = Mathf.Atan2(-slashToEnemy.x, slashToEnemy.y) * Mathf.Rad2Deg;
         
            //    slash.transform.rotation = Quaternion.Euler(0, 0, angle);
            
          
            Debug.Log("Ban");
        }
    }

    public void OnBuff(InputAction.CallbackContext context)
    {
        if (!IsBuff)
        {
            if (context.started)
            {
                IsBuff = true;
                StartCoroutine(buffCoolDown());
            }
        }
       
    }
    void BuffAttack()
    {    GameObject buffAttack= Instantiate(buffAttackPrefabs, buffAttackPoint.position, transform.rotation);
        buffAttack.transform.localScale=(PlayerController.instance.IsFacingRight)?new Vector2(1,1):new Vector2(-1,1);
        Destroy(buffAttack,.5f);
    }
    IEnumerator buffCoolDown()
    {
        yield return new WaitForSeconds(2f);      
        IsBuff = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRange);

        Gizmos.DrawSphere(slashPoint.position, slashAttackRange);
    }
    void FindingEnemy()
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(slashPoint.position, slashAttackRange, enemyLayer);
        var enemy = enemies.OrderBy(item => Vector2.Distance(item.transform.position, this.transform.position)).FirstOrDefault();
        if(enemy != null)
        {
            Debug.Log(enemy.name);
            slashToEnemy = (enemy.transform.position - slashPoint.position).normalized;
           // float angle = Mathf.Atan2(slashToEnemy.x, slashToEnemy.y)*Mathf.Rad2Deg;
            //if (angle != 0)
            //{
            //    this.transform.rotation = Quaternion.Euler(0, 0, angle);
            //}
          
        }
        else
        {
            slashToEnemy = (PlayerController.instance.IsFacingRight) ? Vector2.right : Vector2.left;
        }
         PlayerController.instance.SetFacingDirection(slashToEnemy);
      

    }
   


    public void Attacking()
    {

        if (attackPoint == null)
        {
            return;
        }
        Collider2D[] HitsEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (var enemy in HitsEnemies)
        {
            Debug.Log("Enemy: " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(playerDamage);
        }
    }

}
