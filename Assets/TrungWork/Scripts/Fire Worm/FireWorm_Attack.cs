using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorm_Attack : StateMachineBehaviour
{
    private FireWormController fireWormController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //fireWormController=GameObject.FindAnyObjectByType<FireWormController>().GetComponent<FireWormController>();
        //fireWormController.Spray();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
