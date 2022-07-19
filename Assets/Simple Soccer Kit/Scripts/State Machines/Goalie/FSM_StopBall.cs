using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_StopBall : StateMachineBehaviour
{
    private Goalie _goalie;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _goalie = animator.GetComponent<Goalie>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _goalie.direction = (BallManager.Instance.transform.position - _goalie.transform.position).normalized;
        
        if(!GameManager.Instance.ballShot && Vector3.Distance(_goalie.homePosition, BallManager.Instance.transform.position) > 10)
            animator.SetBool("Stop Ball", false);
        
        if(!GameManager.Instance.gameActive)
            animator.SetBool("Stop Ball", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
