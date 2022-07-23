using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TeamEnum
{
    Home,
    Away
}

public class Team : MonoBehaviour
{
    public int score;
    public List<Player> teamPlayers;
    
    public List<Player> currentUser;

    public ShotTarget shotTarget;
    public Team otherTeam;
    public bool user;
    public bool hasPossession;
    public bool seekingBall;

    public TeamEnum teamEnum;

    public DangerZone dangerZone;
    public MidZone midZone;


    private void Awake()
    {
        teamPlayers = GetComponentsInChildren<Player>().ToList();
        shotTarget = GetComponentInChildren<ShotTarget>();
        dangerZone = GetComponentInChildren<DangerZone>();
        midZone = GetComponentInChildren<MidZone>();
    }
}
