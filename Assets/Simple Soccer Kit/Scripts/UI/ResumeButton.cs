using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    private Button _button;
    
    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(GameManager.Instance.ResumeGame);
        _button.onClick.AddListener(GameManager.Instance.UnloadPauseScene);
    }
}
