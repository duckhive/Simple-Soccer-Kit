using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchSceneButtonClick : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneToUnload;
    
    private Button _button;
    
    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Wrapper);
    }

    private void Wrapper()
    {
        StartCoroutine(LoadAndUnloadScene());
    }
    
    private IEnumerator LoadAndUnloadScene()
    {
        yield return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(sceneToUnload);
    }
}