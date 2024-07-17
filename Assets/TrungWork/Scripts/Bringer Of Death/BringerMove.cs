using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerMove : StateMachineBehaviour
{
    private Demon demon;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        demon=GameObject.FindAnyObjectByType<Demon>().GetComponent<Demon>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        demon.FlipDirection();
    }
}
