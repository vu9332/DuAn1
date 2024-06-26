using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [Range(0f, 10f)]
    public float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float playerDamage;

    //Combo
    [SerializeField]
    private bool _isAttackCombo = true;
    public bool IsAttackComBo { get { return _isAttackCombo; } private set { _isAttackCombo = value; } }

    // Nomarl
     [SerializeField]
    private bool _normalAttack = true;
    public bool IsNormalAttack { get { return _normalAttack; } private set { _normalAttack = value; } }



    [SerializeField]
    private bool _canAttack = true;
    public bool CanAttack { get { return _canAttack; } private set { _canAttack = value; } }


    TouchingDirection touchingDirection;
    Animator myAnimation;
    void Start()
    {
        myAnimation = GetComponent<Animator>();
        touchingDirection=GetComponent<TouchingDirection>();    
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started&& touchingDirection.IsGround&&!PlayerController.instance.IsRolling)
        {
            if (CanAttack&&IsNormalAttack)
            {             
                CanAttack = false;
                IsNormalAttack = false;
                myAnimation.SetTrigger(AnimationString.IsNormalAttack);
            }
        }
        else if (context.canceled)
        {
            IsNormalAttack = true;
            StartCoroutine(AttackCoolDown());
        }
    }

    public void OnSkillOne(InputAction.CallbackContext context)
    {

        if (context.started&&touchingDirection.IsGround && !PlayerController.instance.IsRolling)
        {
            if (IsAttackComBo && CanAttack)
            {
                CanAttack = false;
                IsAttackComBo = false;
                myAnimation.SetTrigger(AnimationString.IsComboAttack);
                if (PlayerController.instance.canReceiveInput)
                {
                    PlayerController.instance.inputReceive = true;
                    PlayerController.instance.canReceiveInput = false;
                }
                else
                {
                    return;
                }

            }

        }
        else if (context.canceled)
        {

            IsAttackComBo = true;
            StartCoroutine(AttackCoolDown());
        }
    }
    IEnumerator AttackCoolDown()
    {
        //float timer = 0;
        //float time = 1.5f;
        //while (time>0)
        //{
        //    time -= Time.deltaTime;
        //    yield return null;

        //}
        yield return new WaitForSeconds(.25f);
        CanAttack = true;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRange);
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
