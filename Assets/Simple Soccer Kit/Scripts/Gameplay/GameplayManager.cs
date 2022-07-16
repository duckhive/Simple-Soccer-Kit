using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    
    public Text homeTeamScoreText;
    public Text awayTeamScoreText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void OnEnable()
    {
        GameManager.Instance.StartGame();
    }

    private void Update()
    {
        ScoreHandler();
        PauseHandler();
        GameOverHandler();
    }

    public void ScoreHandler()
    {
        if (GameManager.Instance.gameActive)
        {
            homeTeamScoreText.text = $"Home: {GameManager.Instance.homeTeam.score}";
            awayTeamScoreText.text = $"Away: {GameManager.Instance.awayTeam.score}";
        }
    }

    public void PauseHandler()
    {
        if(GameManager.Instance.gameActive && Input.GetButtonDown("Pause"))
            GameManager.Instance.PauseGame();
    }

    public void GameOverHandler()
    {
        if(GameManager.Instance.timer == 0 && !SceneManager.GetSceneByName("Game Over").isLoaded)
            GameManager.Instance.GameOver();
    }
    
}
