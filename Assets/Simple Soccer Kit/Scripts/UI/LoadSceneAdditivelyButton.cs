using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneAdditivelyButton : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    
    private Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadSceneAdditively);
    }
    
    
    private void LoadSceneAdditively()
    {
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }
}