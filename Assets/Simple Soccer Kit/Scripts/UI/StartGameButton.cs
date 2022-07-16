using System;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    private Button _button;
    
    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(GameManager.Instance.StartGame);
        _button.onClick.AddListener(GameManager.Instance.LoadGameplayScene);
    }

    private void Update()
    {
        StartGameOnGamepadInput();
    }

    private void StartGameOnGamepadInput()
    {
        if (GameManager.Instance.awayTeam.user || GameManager.Instance.homeTeam.user)
        {
            if (Input.GetButtonDown("Submit"))
            {
                GameManager.Instance.StartGame();
                GameManager.Instance.LoadGameplayScene();
            }
        }
    }
}