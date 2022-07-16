using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject awayTeamWinsText;
    [SerializeField] private GameObject homeTeamWinsText;
    [SerializeField] private GameObject tieText;
    
    public static GameOverManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
    }

    private void Update()
    {
        if (GameManager.Instance.awayTeam.score > GameManager.Instance.homeTeam.score)
            awayTeamWinsText.SetActive(true);
        else if (GameManager.Instance.awayTeam.score < GameManager.Instance.homeTeam.score)
            homeTeamWinsText.SetActive(true);
        else
            tieText.SetActive(true);
    }
}
