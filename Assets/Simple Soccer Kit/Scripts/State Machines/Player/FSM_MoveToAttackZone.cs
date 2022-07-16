using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_MoveToAttackZone : StateMachineBehaviour
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
        _player.direction = (_player.zoneCurrentlySeeking.transform.position - _player.transform.position).normalized;
        
        if (_player.receivingPass)
        {
            animator.SetBool("Receiving Pass", true);
        }
        
        if (Vector3.Distance(_player.zoneCurrentlySeeking.transform.position, _player.transform.position) < 2)
        {
            animator.SetBool("Attack Zone Chosen", false);
        }

        if(_player.hasPossession)
            animator.SetBool("Has Possession", true);
        
        if(_player.otherTeam.hasPossession)
            animator.SetBool("Team Has Possession", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
