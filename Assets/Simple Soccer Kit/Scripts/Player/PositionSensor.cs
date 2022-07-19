using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PositionSensor : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.gameActive)
        {
            if (other.GetComponent<PitchZone>() != null)
            {
                var pitchZone = other.GetComponent<PitchZone>();

                if (pitchZone != null)
                {
                    if (_player.team.teamEnum == TeamEnum.Away)
                    {
                        pitchZone.IncreaseAwayScore();

                        if (pitchZone.teamZoneEnum == TeamZoneEnum.Home)
                            _player.canShoot = true;
                        else
                            _player.canShoot = false;
                    }

                    if (_player.team.teamEnum == TeamEnum.Home)
                    {
                        pitchZone.IncreaseHomeScore();

                        if (pitchZone.teamZoneEnum == TeamZoneEnum.Away)
                            _player.canShoot = true;
                        else
                            _player.canShoot = false;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameManager.Instance.gameActive)
        {
            if (other.GetComponent<PitchZone>() != null)
            {
                var pitchZone = other.GetComponent<PitchZone>();
            
                if(pitchZone != null)
                {
                    if (_player.team.teamEnum == TeamEnum.Away)
                    {
                        pitchZone.DecreaseAwayScore();
                    }

                    if (_player.team.teamEnum == TeamEnum.Home)
                    {
                        pitchZone.DecreaseHomeScore();
                    }
                }
            }
        }
    }
}
