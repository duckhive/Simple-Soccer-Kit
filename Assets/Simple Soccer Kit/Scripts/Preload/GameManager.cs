using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public List<Player> allPlayers;
    public List<Player> possessingPlayer;

    public Team homeTeam;
    public Team awayTeam;

    public float timer;

    public bool gameActive;
    public bool ballShot;

    private float _resetTimer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        
        allPlayers = GetComponentsInChildren<Player>().ToList();
    }

    private void Start()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public IEnumerator GoalScored()
    {
        ballShot = false;
        
        GameNotActive();
        
        yield return new WaitForSeconds(1f);
        
        ResetBallAndPlayers();

        yield return new WaitForSeconds(1f);

        GameActive();
    }

    public void ResetBallAndPlayers()
    {
        BallManager.Instance.ResetBall();

        foreach (Player player in allPlayers)
            player.ResetPositionAndRotation();

        homeTeam.hasPossession = false;
        awayTeam.hasPossession = false;
    }
    

    private void GameActive()
    {
        gameActive = true;
    }

    private void GameNotActive()
    {
        gameActive = false;
    }

    public void StartGame()
    {
        _resetTimer = timer;
        ResetBallAndPlayers();
        Invoke(nameof(GameActive), 1);
        
        GetComponentsInChildren<PlayerSwitching>()
            .Where(t => t.team.user)
            .FirstOrDefault()?
            .SelectPlayerOnStart();
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        GameNotActive();
        SceneManager.LoadSceneAsync("Pause Screen", LoadSceneMode.Additive);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameActive();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        homeTeam.score = 0;
        awayTeam.score = 0;
        timer = _resetTimer;
        LoadGameplayScene();
        StartGame();
        
        foreach (Player player in allPlayers)
            player.AiBrain();
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        awayTeam.user = false;
        homeTeam.user = false;
        homeTeam.score = 0;
        awayTeam.score = 0;
        timer = _resetTimer;
        LoadMainMenuScene();
        
        foreach (Player player in allPlayers)
            player.AiBrain();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameNotActive();
        SceneManager.LoadScene("Game Over", LoadSceneMode.Additive);
    }

    #region Scene Management
    
    public void UnloadPauseScene()
    {
        SceneManager.UnloadSceneAsync("Pause Screen");
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void LoadGameplayScene()
    {
        SceneManager.LoadSceneAsync("Gameplay");
    }

    #endregion
}
