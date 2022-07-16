using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneButtonPress : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneToUnload;
    [SerializeField] private string buttonToSwitchScene;

    private void Update()
    {
        if (Input.GetButtonDown(buttonToSwitchScene))
            StartCoroutine(LoadAndUnloadScene());
    }

    private IEnumerator LoadAndUnloadScene()
    {
        yield return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(sceneToUnload);
    }
}