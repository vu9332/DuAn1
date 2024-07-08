using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerMove : StateMachineBehaviour
{
    private BringerOfDeath bringerOfDeath;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bringerOfDeath=GameObject.FindAnyObjectByType<BringerOfDeath>().GetComponent<BringerOfDeath>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bringerOfDeath.foundPlayer)
        {
            animator.SetTrigger("Attack");
        }
        bringerOfDeath.Flip();
        bringerOfDeath.MoveToPlayer();
    }
}
