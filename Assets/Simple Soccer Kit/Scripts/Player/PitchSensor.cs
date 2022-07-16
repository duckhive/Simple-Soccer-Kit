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
        if (other.TryGetComponent<PitchZone>(out PitchZone pitchZone) != null)
            _player.nearbyPitchZones.Insert(0, pitchZone);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PitchZone>(out PitchZone pitchZone) != null)
            _player.nearbyPitchZones.Remove(pitchZone);
    }
    
    public PitchZone CalculateBestSupportZone()
    {
        var zones = _player.nearbyPitchZones;

        foreach (PitchZone zone in zones)
        {
            if (zone == null)
                zones.Remove(zone);
            if (zone.awayScore > 0)
                zones.Remove(zone);
        }

        var randomZone = Random.Range(0, zones.Count);

        return zones[randomZone];
    }
}
