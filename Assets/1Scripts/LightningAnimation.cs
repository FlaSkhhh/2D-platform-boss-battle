using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAnimation : StateMachineBehaviour
{
    public float count = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (count < 5)
        {
            count++;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(count == 5)
        {
            count = 0;
            animator.SetBool("Lightning", false); ;
            animator.SetTrigger("LightningRain");
        }

        if (!animator.GetBool("Lightning"))
        {
            count = 0;
        }
    }
}
