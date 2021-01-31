using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider slider;

    public string sceneName = "GameScene";
    public float transitionTime = 1f;
    

    public void PlayGame()
    {
        StartCoroutine(LoadingScene(sceneName, transitionTime));
    }

    IEnumerator LoadingScene(string sceneName, float transitionTime)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }
}
