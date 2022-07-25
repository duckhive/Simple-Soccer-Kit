using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_AdvanceBall : StateMachineBehaviour
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
        if(_player.isOpen)
            _player.direction = (_player.zoneCurrentlySeeking.transform.position - _player.transform.position).normalized;
        else 
            _player.direction = (_player.transform.position - _player.playerCovering.transform.position).normalized;

        if (Vector3.Distance(_player.zoneCurrentlySeeking.transform.position, _player.transform.position) < 2)
        {
            animator.SetBool("Advance Zone Chosen", false);
        }
        
        if(_player.zoneCurrentlySeeking == null)
            animator.SetBool("Advance Zone Chosen", false);

        if (_player.team.hasPossession && !_player.hasPossession)
        {
            animator.SetBool("Team Has Possession", true);
            animator.SetBool("Has Possession", false);
        }

        if (!_player.team.hasPossession)
        {
            animator.SetBool("Team Has Possession", false);
            animator.SetBool("Has Possession", false);
        }

        if (!_player.isOpen && _player.hasPossession)
        {
            if (Random.value > 0.99)
            {
                animator.ResetTrigger("Pass");
                animator.SetTrigger("Pass");
            }
        }

        if (/*_player.isOpen && */_player.canShoot)
        {
            if (Random.value > 0.95)
            {
                animator.ResetTrigger("Shoot");
                animator.SetTrigger("Shoot");
            }
        }

        if (!_player.isOpen && _player.distanceToGoal < 25)
        {
            animator.ResetTrigger("Shoot");
            animator.SetTrigger("Shoot");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
