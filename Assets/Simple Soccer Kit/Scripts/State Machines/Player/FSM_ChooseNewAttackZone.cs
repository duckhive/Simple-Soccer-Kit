using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FSM_ChooseNewAttackZone : StateMachineBehaviour
{
    private Player _player;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.GetComponent<Player>();
        ChooseNewAttackZone(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_player.hasPossession)
            animator.SetBool("Has Possession", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
    private void ChooseNewAttackZone(Animator animator)
    {
        if (_player.team.teamEnum == TeamEnum.Home)
        {
            var bestZones = PitchManager.Instance.pitchZones
                //.Where(t => t.awayScore == 0)
                //.Where(t => t.homeScore == 0)
                .Where(t => Vector3.Distance(t.transform.position, BallManager.Instance.transform.position) < 30)
                .Where(t => Vector3.Distance(t.transform.position, BallManager.Instance.transform.position) > 10)
                //.Where(t => t.transform.position.z >= _player.transform.position.z)
                //.Where(t => t.transform.position.z >= BallManager.Instance.transform.position.z)
                .ToList();
            
            if (bestZones[0] != null)
            {
                var randomZone = Random.Range(0, bestZones.Count);

                _player.zoneCurrentlySeeking = bestZones[randomZone];
                
                animator.SetBool("Attack Zone Chosen", true);
            }
            else
            {
                _player.zoneCurrentlySeeking = _player.attackZone;
                
                animator.SetBool("Attack Zone Chosen", true);
            }
        }
        
        if (_player.team.teamEnum == TeamEnum.Away)
        {
            var bestZones = PitchManager.Instance.pitchZones
                //.Where(t => t.awayScore == 0)
                //.Where(t => t.homeScore == 0)
                .Where(t => Vector3.Distance(t.transform.position, BallManager.Instance.transform.position) < 30)
                .Where(t => Vector3.Distance(t.transform.position, BallManager.Instance.transform.position) > 10)
                //.Where(t => t.transform.position.z <= _player.transform.position.z)
                //.Where(t => t.transform.position.z <= BallManager.Instance.transform.position.z)
                .ToList();
            
            if (bestZones[0] != null)
            {
                var randomZone = Random.Range(0, bestZones.Count);

                _player.zoneCurrentlySeeking = bestZones[randomZone];
                
                animator.SetBool("Attack Zone Chosen", true);
            }
            else
            {
                _player.zoneCurrentlySeeking = _player.attackZone;
                
                animator.SetBool("Attack Zone Chosen", true);
            }
        }
    }
}
