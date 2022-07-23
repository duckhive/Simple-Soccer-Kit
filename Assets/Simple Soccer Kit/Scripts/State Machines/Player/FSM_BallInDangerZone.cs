using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_BallInDangerZone : StateMachineBehaviour
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
        _player.direction = (_player.dangerZone.transform.position - _player.transform.position).normalized;
        
        if(!_player.team.hasPossession && !_player.otherTeam.hasPossession)
            animator.SetBool("Seeking Ball", true);
        
        if (!_player.team.dangerZone.ballInDangerZone)
        {
            animator.SetBool("Seek Danger Zone", false);
        }
        
        if (Vector3.Distance(BallManager.Instance.transform.position, _player.homeZone.transform.position) < 15
            && !_player.team.seekingBall)
        {
            animator.SetBool("Seeking Ball", true);
        }
        
        if(_player.team.hasPossession)
            animator.SetBool("Team Has Possession", true);
        
        if(_player.team.midZone.ballInMidZone)
            animator.SetBool("Seek Mid Zone", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
