using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamZoneEnum
{
    Home,
    Away,
    Neutral
}

public class PitchZone : MonoBehaviour
{
    public float awayScore;
    public float homeScore;
    public TeamZoneEnum teamZoneEnum;

    public void IncreaseAwayScore()
    {
        awayScore++;
    }
    
    public void IncreaseHomeScore()
    {
        homeScore++;
    }

    public void DecreaseAwayScore()
    {
        awayScore--;
    }
    
    public void DecreaseHomeScore()
    {
        homeScore--;
    }
}
