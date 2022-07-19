using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FSM_ChooseZoneToAdvanceTo : StateMachineBehaviour
{
    private Player _player;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.GetComponent<Player>();
        
        ChooseZoneToAdvance(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
    private void ChooseZoneToAdvance(Animator animator)
    {
        if (_player.team.teamEnum == TeamEnum.Home)
        {
            var bestZones = PitchManager.Instance.pitchZones
                .Where(t => t.awayScore == 0)
                .Where(t => t.transform.position.z > _player.transform.position.z)
                .ToList();


            if (bestZones[0] != null)
            {
                var randomZone = Random.Range(0, bestZones.Count);

                _player.zoneCurrentlySeeking = bestZones[randomZone];
                
                animator.SetBool("Advance Zone Chosen", true);
            }
            else
            {
                _player.zoneCurrentlySeeking = _player.attackZone;
                animator.SetBool("Advance Zone Chosen", true);
            }
        }
        
        if (_player.team.teamEnum == TeamEnum.Away)
        {
            var bestZones = PitchManager.Instance.pitchZones
                .Where(t => t.homeScore == 0)
                .Where(t => t.transform.position.z < _player.transform.position.z)
                .ToList();

            
            if (bestZones[0] != null)
            {
                var randomZone = Random.Range(0, bestZones.Count);

                _player.zoneCurrentlySeeking = bestZones[randomZone];
                
                animator.SetBool("Advance Zone Chosen", true);
            }
            else
            {
                _player.zoneCurrentlySeeking = _player.attackZone;
                animator.SetBool("Advance Zone Chosen", true);
            }
        }
    }
}
