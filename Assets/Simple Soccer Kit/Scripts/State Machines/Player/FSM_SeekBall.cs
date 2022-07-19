using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_SeekBall : StateMachineBehaviour
{
    private Player _player;
    private Vector3 _targetDir;
    private float _lookAhead;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.GetComponent<Player>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _targetDir = (BallManager.Instance.transform.position - _player.transform.position).normalized;
        _lookAhead = _targetDir.magnitude /
                     (_player.rb.velocity.magnitude + BallManager.Instance.rb.velocity.magnitude);
        
        _player.direction = ((BallManager.Instance.transform.position + BallManager.Instance.rb.velocity * _lookAhead) - _player.transform.position).normalized;
        
        //_player.direction = (BallManager.Instance.transform.position - _player.transform.position).normalized;

        if (_player.team.hasPossession)
        {
            animator.SetBool("Team Has Possession", true);
            animator.SetBool("Seeking Ball", false);
        }

        if (_player.team.dangerZone.ballInDangerZone)
        {
            if (Vector3.Distance(BallManager.Instance.transform.position, _player.dangerZone.transform.position) > 18)
            {
                animator.SetBool("Seek Danger Zone", true);
                animator.SetBool("Seeking Ball", false);
            }
        }
        else if (_player.team.midZone.ballInMidZone)
        {
            if (Vector3.Distance(BallManager.Instance.transform.position, _player.midZone.transform.position) > 18)
            {
                animator.SetBool("Seek Mid Zone", true);
                animator.SetBool("Seeking Ball", false);
            }
        }
        else
        {
            if (Vector3.Distance(BallManager.Instance.transform.position, _player.homeZone.transform.position) > 18)
            {
                animator.ResetTrigger("Seek Home Zone");
                animator.SetTrigger("Seek Home Zone");
                animator.SetBool("Seeking Ball", false);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
