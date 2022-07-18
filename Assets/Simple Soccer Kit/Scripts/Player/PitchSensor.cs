using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PitchSensor : MonoBehaviour
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
                _player.nearbyPitchZones.Insert(0, pitchZone);
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
                _player.nearbyPitchZones.Remove(pitchZone);
            }
        }
    }
}
