using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    private Team _team;
    private Player _player;

    private void Awake()
    {
        _team = GetComponentInParent<Team>();
        _player = GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<BallManager>() && GameManager.Instance.gameActive && !_player.possessionCooldown && !_player.team.hasPossession && !GameManager.Instance.ballShot)
        {
            StartCoroutine(GainPossession());
        }
    }

    private IEnumerator GainPossession()
    {
        BallManager.Instance.transform.position = _player.ballPosition.position;
        BallManager.Instance.transform.SetParent(_player.ballPosition);
        BallManager.Instance.rb.constraints = RigidbodyConstraints.FreezeAll;
        
        GameManager.Instance.possessingPlayer.Insert(0, _player);
        if(GameManager.Instance.possessingPlayer.Count > 2)
            GameManager.Instance.possessingPlayer.RemoveAt(2);
        
        GameManager.Instance.homeTeam.shotTarget.ResetPosition();
        GameManager.Instance.awayTeam.shotTarget.ResetPosition();

        foreach (Player player in GameManager.Instance.allPlayers)
        {
            player.hasPossession = false;
            player.receivingPass = false;
        }
        
        _player.hasPossession = true;
        _player.team.otherTeam.hasPossession = false;
        _player.team.hasPossession = true;

        if(_player.team.user)
            SwapBrains();

        yield return null;

        if (GameManager.Instance.possessingPlayer.Count > 1)
            StartCoroutine(PossessionCooldown(GameManager.Instance.possessingPlayer[1]));
    }

    private void SwapBrains()
    {
        if (_team.currentUser[0] != _player)
        {
            _player.UserBrain();
            _team.currentUser[1].AiBrain();
        }
    }

    public IEnumerator PossessionCooldown(Player player)
    {
        player.possessionCooldown = true;

        yield return new WaitForSeconds(0.5f);

        player.possessionCooldown = false;
    }
}
