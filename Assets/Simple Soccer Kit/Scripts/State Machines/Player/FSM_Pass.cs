using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FSM_Pass : StateMachineBehaviour
{
    private Player _player;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.GetComponent<Player>();
        _player.team.GetComponent<Passing>().PassBallAi((FindPlayerToPassTo().transform.position - BallManager.Instance.transform.position).normalized);
        FindPlayerToPassTo().receivingPass = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!_player.hasPossession)
            animator.SetBool("Has Possession", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
    private Player FindPlayerToPassTo()
    {
        var openPlayer = _player.team.teamPlayers
            .Where(t => t != _player)
            .Where(t => t.isOpenForPass)
            .Where(t=> Vector3.Distance(t.transform.position, _player.transform.position) > 5)
            .OrderBy(t => t.distanceToGoal)
            .FirstOrDefault();

        if (openPlayer != null)
        {
            return openPlayer;
        }
        else
        {
            return _player.team.teamPlayers
                .Where(t => t != _player)
                .OrderByDescending(t => t.distanceToGoal)
                .FirstOrDefault();
        }
    }
}
