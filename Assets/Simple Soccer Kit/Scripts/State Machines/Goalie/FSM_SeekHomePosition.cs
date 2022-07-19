using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_SeekHomePosition : StateMachineBehaviour
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
        if (!GameManager.Instance.ballShot)
        {
            if(Vector3.Distance(_goalie.transform.position, _goalie.homePosition) > 1)
                _goalie.direction = (_goalie.homePosition - _goalie.transform.position).normalized;
            else if(Vector3.Distance(BallManager.Instance.transform.position, _goalie.homePosition) < 10 
                    && !_goalie.team.hasPossession)
                animator.SetBool("Stop Ball", true);
            else 
                _goalie.direction = Vector3.zero;
        }
        
        else
        {
            animator.SetBool("Stop Ball", true);
        }    
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
