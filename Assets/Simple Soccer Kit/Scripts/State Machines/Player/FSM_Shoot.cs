using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Shoot : StateMachineBehaviour
{
    private Player _player;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.GetComponent<Player>();
        ShootBall();
        animator.SetBool("Has Possession", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
    public void ShootBall()
    {
        var direction = ((_player.team.shotTarget.transform.position + new Vector3(Random.Range(-5, 5), Random.Range(2, 5), 0)) - BallManager.Instance.transform.position).normalized;

        BallManager.Instance.rb.constraints = RigidbodyConstraints.None;
        BallManager.Instance.transform.SetParent(null);
        BallManager.Instance.rb.AddForce(direction * _player.GetComponent<Shooting>().shotForce, ForceMode.Impulse);
        _player.hasPossession = false;
        _player.team.hasPossession = false;
        GameManager.Instance.ballShot = true;
    }
}
