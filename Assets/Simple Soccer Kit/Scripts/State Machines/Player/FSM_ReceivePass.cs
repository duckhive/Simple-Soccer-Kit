using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_ReceivePass : StateMachineBehaviour
{
    private Player _player;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.GetComponent<Player>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_player.receivingPass)
            _player.direction = (BallManager.Instance.transform.position - _player.transform.position).normalized;
        else 
            _player.direction = Vector3.zero;
        
        if (_player.hasPossession)
        {
            animator.SetBool("Has Possession", true);
            animator.SetBool("Receiving Pass", false);
        }

        if (_player.team.hasPossession && !_player.hasPossession)
        {
            animator.SetBool("Has Possession", false);
            animator.SetBool("Receiving Pass", false);
        }

        if (_player.otherTeam.hasPossession)
        {
            animator.SetBool("Team Has Possession", false);
            animator.SetBool("Receiving Pass", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
