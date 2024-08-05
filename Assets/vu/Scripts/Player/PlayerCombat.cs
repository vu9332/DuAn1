using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;



public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat Instance { get; set; }

    [Header("NormaAttack (Left Mouse) ")]
    [SerializeField] private bool trigger;
    [SerializeField] private float combo;
    [SerializeField] private float comboTiming = .5f;
    [SerializeField] private float comboTempo;
    [SerializeField] private float attackNormalCoolDown;
    public float maxDistanceWhileAttack;
    Vector2 startMoveWhileAttackPos;
    int comboNumber = 3;
    // Nomarl
    [SerializeField]
    private bool _normalAttack = false;
    public bool IsNormalAttack { get { return _normalAttack; } set { _normalAttack = value; } }


    // Chieu 1
    [Header("Skill 1 ( F )")]
    [SerializeField] public SkillData skillOneData;
    [SerializeField] float maxDistanceSkillOneSlide;
    private Vector2 startPointSlideSkillOne { get; set; }
    [SerializeField]

    public bool IsSkillOne { get { return skillOneData._DoSkill; } set { skillOneData._DoSkill = value; } }

    // Chieu 2
    [Header("Chieu 2 ( Q )")]
    [SerializeField] private Transform slashPoint;
    [SerializeField] public SkillData skillTwoData;
    private Vector3 slashToEnemy;
    [Range(0, 100)] public float slashAttackRange;

    public bool IsSkillTwo { get { return skillTwoData._DoSkill; } set { skillTwoData._DoSkill = value; } }



    // Chieu 3
    [Header(" Chieu 3 ( R )")]
    [SerializeField] public SkillData skillThreeData;
    [SerializeField] private float maxDistanceKnock;
    [SerializeField] private float force;
    [SerializeField] private Transform skillThreeAttackPoint;
    private Vector2 startPosknock;
    private bool doSkillThree = false;

    public bool IsSkillThree { get { return skillThreeData._DoSkill; } set { skillThreeData._DoSkill = value; } }



    [SerializeField]
    private bool _canAttack = true;
    public bool CanAttack { get { return _canAttack; } set { _canAttack = value; } }

    Knockback knockback;
    TouchingDirection touchingDirection;
    Animator myAnimator;
    Rigidbody2D rb;
    PlayerHealth playerHealth;
    SkillManager skillManager;
    //SkillOne skillOne;
    TrailRenderer trailRenderer;
    [SerializeField] private List<AudioClip> attackSoundClip;



    [SerializeField] private Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject particleOnHitPrefabVFX;


  
    public float playerDamage
    {
        get
        {
            if (!IsSkillOne)
                return skillOneData.damage;
            else
                return PlayerController.Instance.playerData.playerDamage;
        }
        private set { PlayerController.Instance.playerData.playerDamage = value; }
    }

    // public bool CanMove { get { return myAnimator.GetBool(AnimationString.canMove); } }
    

    void Start()
    {
        // skillTwo=GetComponent<SkillTwo>();
        //  skillOne = GetComponent<SkillOne>();
        knockback = GetComponent<Knockback>();
        playerHealth = GetComponent<PlayerHealth>();
        myAnimator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        rb = GetComponent<Rigidbody2D>();
        comboTempo = comboTiming;
        trailRenderer = GetComponent<TrailRenderer>();
        skillManager = GetComponent<SkillManager>();
        if(Instance==null)
            Instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        NormalAttackSlide();
        SK1Slide();
        comboTempo -= Time.deltaTime;
        if (comboTempo < 0)
        {
            combo = 1;
        }
    }
    private void FixedUpdate()
    {
       
    }
    #region normal attack
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if ((!PlayerController.Instance.IsRolling && !PlayerController.Instance.IsDash) && touchingDirection.IsGround && playerHealth.currentStamina > 1)
            {
              
                rb.velocity = Vector2.zero;
                if (context.started && comboTempo < 0 &&IsNormalAttack)
                {
                    //  SoundFXManagement.Instance.PlaySoundFXClip(attackSoundClip[3], transform, .5f);
                    playerHealth.currentStamina -= 1;
                    IsNormalAttack = false;
                    startMoveWhileAttackPos = rb.position;
                    StartCoroutine(NormalAttackCoolDown(attackNormalCoolDown));
                    myAnimator.SetTrigger(AnimationString.IsNormalAttack + combo);
                    comboTempo = comboTiming;

                }
                else if (context.started && (comboTempo > 0 && comboTempo <= 1f) &&IsNormalAttack)
                {
                    // SoundFXManagement.Instance.PlaySoundFXClip(attackSoundClip[3], transform, .5f);

                    IsNormalAttack = false;
                    combo++;
                    if (combo > comboNumber)
                    {
                        combo = 1;
                    }
                    playerHealth.currentStamina -= 1;
                    startMoveWhileAttackPos = rb.position;
                    myAnimator.SetTrigger(AnimationString.IsNormalAttack + combo);
                    comboTempo = comboTiming;
                    StartCoroutine(NormalAttackCoolDown(attackNormalCoolDown));

                }
                else if (comboTempo < 0 && context.canceled)
                {
                    IsNormalAttack = true;
                }

            }
            else if (!touchingDirection.IsGround && context.started && IsNormalAttack)
            {
                IsNormalAttack = false;PlayerController.Instance. moveInput = Vector2.zero;
                myAnimator.SetTrigger(AnimationString.IsAirAttack);
                playerHealth.currentStamina -= 1;
                StartCoroutine(NormalAttackCoolDown(attackNormalCoolDown));
            }
        }

    }
    void NormalAttackSlide()
    {
        if (!IsNormalAttack)
        {

            if (Vector2.Distance(startMoveWhileAttackPos, this.transform.position) <= maxDistanceWhileAttack)
            {

                rb.AddForce(new Vector2(PlayerController.Instance.currentMoveSpeed, 0), ForceMode2D.Impulse);

            }
            else rb.velocity = Vector2.zero;

        }
    }
    public void PlayNormalAttackAudioClip()
    {
        SoundFXManagement.Instance.PlaySoundFXClip(attackSoundClip[0], transform, 1f);
    }
    IEnumerator NormalAttackCoolDown(float coolDownTime)
    {

       
        yield return new WaitForSeconds(coolDownTime);
        IsNormalAttack = true;
    }
   
    #endregion

    #region Skill 1
    public void OnSkillOne(InputAction.CallbackContext context)
    {



        if (context.performed && TouchingDirection.Instance.IsGround && playerHealth.currentStamina > 2)
        {

            // rb.velocity = Vector2.zero;
            if (IsSkillOne && CanAttack && (!PlayerController.Instance.IsRolling && !PlayerController.Instance.IsDash))
            {
                Debug.Log("Chiêu 1 ");
                ActivateSkillOne();

            }
        }


    }
    void SK1Slide()
    {
       
      
       if(!IsSkillOne&&!CanAttack)
        {

            if (Vector2.Distance(startPointSlideSkillOne, this.transform.position) <= maxDistanceSkillOneSlide)
            {
                rb.AddForce(new Vector2(PlayerController.Instance.currentMoveSpeed * 10, 0), ForceMode2D.Impulse);
                GameObject ff = Instantiate(skillOneData.effectPrefab, transform.position, transform.rotation);
                ff.transform.localScale = this.transform.localScale;
                Destroy(ff, .2f);
            }
            else rb.velocity = Vector2.zero;
        }
       
          
        


    }
    public void ActivateSkillOne()
    {
        if (SkillManager.Instance.IsSkillOneUnlock&& IsSkillOne)
        {
            IsSkillOne = false;
            SoundFXManagement.Instance.PlaySoundFXClip(skillOneData.soundEffect[0], transform, 0.3f);
            // trailRenderer.emitting = true;
            playerHealth.currentStamina -= 2;
            startPointSlideSkillOne = this.transform.position;
            CanAttack = false;
          //  StartCoroutine(SK1Slide());
            IsNormalAttack = false;
            myAnimator.SetTrigger(AnimationString.IsSkillOne);
            StartCoroutine(SkillOneCoolDown(skillOneData.coolDownTime));
            CameraShake.instance.ShakeCamera();
         
        }
        else Debug.Log("Chiêu 1 chưa mở");
    }
    IEnumerator SkillOneCoolDown(float coolDownTime)
    {
        Player_Abilities.skill1AbilityCoolDown();
        yield return new WaitForSeconds(.1f);
        //trailRenderer.emitting = false;
        CanAttack = true;
        IsNormalAttack = true;
        yield return new WaitForSeconds(coolDownTime);
        IsSkillOne = true;
    }

    #endregion

    #region Skill 2


    public void OnSkillTwo(InputAction.CallbackContext context)
    {

        if (context.started && playerHealth.currentStamina > 2)
        {
            if (IsSkillTwo && CanAttack && (!PlayerController.Instance.IsRolling && !PlayerController.Instance.IsDash))
            {

                ActivateSkillTwo();
            }
        }
        CanAttack = true;


    }
    public void ActivateSkillTwo()
    {
        if (SkillManager.Instance.IsSkillTwoUnlock&& IsSkillTwo)
        {           
            SoundFXManagement.Instance.PlaySoundFXClip(skillTwoData.soundEffect[0], transform, .5f);
            playerHealth.currentStamina -= 2;
            IsSkillTwo = false;
            CanAttack = false;
            myAnimator.SetTrigger(AnimationString.IsSkillTwo);
            FindingEnemy();
            StartCoroutine(SkillTwoCoolDown(skillTwoData.coolDownTime));
        }
        else Debug.Log("Chiêu 2 chưa mở");
    }
    IEnumerator SkillTwoCoolDown(float coolDownTime)
    {
        Player_Abilities.Skill2AbilityCoolDown();
        yield return new WaitForSeconds(coolDownTime);
        IsSkillTwo = true;
    }
    public void PlaySlashAttackAudioClip()
    {
        SoundFXManagement.Instance.PlaySoundFXClip(skillTwoData.soundEffect[1], transform, 1f);
    }
    public void Slash()
    {
        GameObject slash = Instantiate(skillTwoData.effectPrefab, slashPoint.position, transform.rotation);
        Slash slashScript = slash.GetComponent<Slash>();
        if (slash != null)
        {
            slashScript.SetSlashDirection(slashToEnemy);
            slashScript.GetSlashDamage(skillTwoData.damage);
            //float angle = Mathf.Atan2(-slashToEnemy.x, slashToEnemy.y) * Mathf.Rad2Deg;

            //    slash.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        CameraShake.instance.ShakeCamera();
    }

    void FindingEnemy()
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(this.transform.position, slashAttackRange, enemyLayer);
        var enemy = enemies.OrderBy(item => Vector2.Distance(item.transform.position, this.transform.position)).FirstOrDefault();
        if (enemy != null)
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
            slashToEnemy = (PlayerController.Instance.IsFacingRight) ? Vector2.right : Vector2.left;
        }
        PlayerController.Instance.SetFacingDirection(slashToEnemy);


    }

    #endregion


    #region Skill 3

    public void OnSkillThree(InputAction.CallbackContext context)
    {

        if (IsSkillThree && CanAttack)
        {
            if (context.performed)
            {


                ActivateSkillThree();

            }


        }

    }
    public void ActivateSkillThree()
    {
        if (SkillManager.Instance.IsSkillThreeUnlock&& IsSkillThree)
        {
            SoundFXManagement.Instance.PlaySoundFXClip(skillThreeData.soundEffect[0], transform, 1f);
            CameraZoom.instance.ZoomIn();
            IsSkillThree = false;
            CanAttack = false;
            myAnimator.SetTrigger(AnimationString.IsSkillThree);
            startPosknock = this.transform.position;
            if (!IsSkillThree)
            {
                foreach (Transform chilld in this.transform)
                {
                    if (chilld.gameObject.name == ("SkillEffect"))
                    {
                        chilld.gameObject.SetActive(true);
                    }
                }

            }
        }
        else Debug.Log("Chiêu 3 chưa mở");

    }
    IEnumerator ActivateEffect()
    {
        yield return null;
        foreach (Transform chilld in this.transform)
        {
            if (chilld.gameObject.name == ("SkillEffect"))
            {
                chilld.gameObject.SetActive(false);
            }
        }
    }
    public void SkillThreeAttack()
    {
        if (!IsSkillThree)
        {
            SoundFXManagement.Instance.PlaySoundFXClip(skillThreeData.soundEffect[1], transform, 1f);
            doSkillThree = true;
            CanAttack = true;
            GameObject buffAttack = Instantiate(skillThreeData.effectPrefab, skillThreeAttackPoint.position, transform.rotation);
            buffAttack.transform.localScale = (PlayerController.Instance.IsFacingRight) ? new Vector2(1, 1) : new Vector2(-1, 1);
            ArrowAttack setDir = buffAttack.GetComponent<ArrowAttack>();
            if (setDir != null)
            {
                setDir.GetDirectionArrowAttack(buffAttack.transform.localScale);
                setDir.GetArrowDamage(skillThreeData.damage);
            }
        }
        rb.AddForce(new Vector2(this.transform.localScale.x * -force, 0), ForceMode2D.Impulse);
        StartCoroutine(calculateDistance());
        StartCoroutine(ActivateEffect());
        CameraShake.instance.ShakeCamera();
        CameraZoom.instance.ZoomOut();
        StartCoroutine(SkillThreeCoolDown());
    }
    IEnumerator calculateDistance()
    {
        while (doSkillThree)
        {
            yield return null;
            if (Vector2.Distance(startPosknock, this.transform.position) >= maxDistanceKnock)
            {
                rb.velocity = Vector2.zero;
                doSkillThree = false;
            }
        }
    }

    IEnumerator SkillThreeCoolDown()
    {
        Player_Abilities.Skill3AbilityCoolDown();
        yield return new WaitForSeconds(skillThreeData.coolDownTime);
        IsSkillThree = true;
    }
    #endregion

    public void Attacking()
    {

        if (attackPoint == null)
        {
            return;
        }
        Collider2D[] HitsEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (var enemy in HitsEnemies)
        {
            if(enemy!=null)
            {
                GameObject ps = Instantiate(particleOnHitPrefabVFX, enemy.transform.position, transform.rotation);
                Destroy(ps, .15f);
                Debug.Log("Enemy: " + enemy.name);
                enemy.GetComponent<Enemy>().TakeDamage(playerDamage);
                CameraShake.instance.ShakeCamera();
            }
          
        }
    }
  
}
