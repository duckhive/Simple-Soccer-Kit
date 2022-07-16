using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text _timerText;

    private void Awake()
    {
        _timerText = GetComponent<Text>();
    }

    private void Update()
    {
        DisplayTimer();
    }

    private void DisplayTimer()
    {
        if (GameManager.Instance.gameActive)
        {
            if (GameManager.Instance.timer > 0)
                GameManager.Instance.timer -= Time.deltaTime;

            if (GameManager.Instance.timer < 0)
                GameManager.Instance.timer = 0;

            float minutes = Mathf.FloorToInt(GameManager.Instance.timer / 60);
            float seconds = Mathf.FloorToInt(GameManager.Instance.timer % 60);

            _timerText.text = $"{minutes:0}:{seconds:00}";
        }
    }
}
