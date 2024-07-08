using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye_AttackPlayer : StateMachineBehaviour
{
    [SerializeField] FlyingEyes flyingEyes;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        flyingEyes = GameObject.FindAnyObjectByType<FlyingEyes>().GetComponent<FlyingEyes>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        flyingEyes.AttackPlayer();
    }
}
