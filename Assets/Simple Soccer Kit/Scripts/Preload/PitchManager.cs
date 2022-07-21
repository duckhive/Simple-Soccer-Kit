using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PitchManager : MonoBehaviour
{
    public static PitchManager Instance;

    public List<PitchZone> pitchZones;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        pitchZones = GetComponentsInChildren<PitchZone>().ToList();
    }
    
    private void Start()
    {
        SetPlayerHomeZones();
        SetPlayerAttackZones();
        SetPlayerDangerZones();
        SetPlayerMidZones();
    }
    
    private void SetPlayerHomeZones()
    {
        GameManager.Instance.allPlayers[0].homeZone = pitchZones[13];
        GameManager.Instance.allPlayers[1].homeZone = pitchZones[14];
        GameManager.Instance.allPlayers[2].homeZone = pitchZones[8];
        GameManager.Instance.allPlayers[3].homeZone = pitchZones[11];
        GameManager.Instance.allPlayers[4].homeZone = pitchZones[22];
        GameManager.Instance.allPlayers[5].homeZone = pitchZones[21];
        GameManager.Instance.allPlayers[6].homeZone = pitchZones[27];
        GameManager.Instance.allPlayers[7].homeZone = pitchZones[24];
    }

    private void SetPlayerMidZones()
    {
        GameManager.Instance.allPlayers[0].midZone = pitchZones[17];
        GameManager.Instance.allPlayers[1].midZone = pitchZones[18];
        GameManager.Instance.allPlayers[2].midZone = pitchZones[12];
        GameManager.Instance.allPlayers[3].midZone = pitchZones[15];
        GameManager.Instance.allPlayers[4].midZone = pitchZones[18];
        GameManager.Instance.allPlayers[5].midZone = pitchZones[17];
        GameManager.Instance.allPlayers[6].midZone = pitchZones[23];
        GameManager.Instance.allPlayers[7].midZone = pitchZones[20];
    }
    
    private void SetPlayerDangerZones()
    {
        GameManager.Instance.allPlayers[0].dangerZone = pitchZones[9];
        GameManager.Instance.allPlayers[1].dangerZone = pitchZones[10];
        GameManager.Instance.allPlayers[2].dangerZone = pitchZones[4];
        GameManager.Instance.allPlayers[3].dangerZone = pitchZones[7];
        GameManager.Instance.allPlayers[4].dangerZone = pitchZones[26];
        GameManager.Instance.allPlayers[5].dangerZone = pitchZones[25];
        GameManager.Instance.allPlayers[6].dangerZone = pitchZones[31];
        GameManager.Instance.allPlayers[7].dangerZone = pitchZones[28];
    }

    private void SetPlayerAttackZones()
    {
        GameManager.Instance.allPlayers[0].attackZone = pitchZones[28];
        GameManager.Instance.allPlayers[1].attackZone = pitchZones[31];
        GameManager.Instance.allPlayers[2].attackZone = pitchZones[21];
        GameManager.Instance.allPlayers[3].attackZone = pitchZones[22];
        GameManager.Instance.allPlayers[4].attackZone = pitchZones[7];
        GameManager.Instance.allPlayers[5].attackZone = pitchZones[4];
        GameManager.Instance.allPlayers[6].attackZone = pitchZones[14];
        GameManager.Instance.allPlayers[7].attackZone = pitchZones[13];
    }
}
