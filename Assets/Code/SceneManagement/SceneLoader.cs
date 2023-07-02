using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScene;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextVisualizer textVisualizer;
    [SerializeField] private string[] suggestions;

    public void LoadGameScene()
    {
        loadingScene.SetActive(true);
        textVisualizer.SetTitleField(textVisualizer.GetComponent<Text>());
        StartCoroutine(WriteTextAfterTime());
        StartCoroutine(LoadSceneAsync("Game"));
    }

    private IEnumerator WriteTextAfterTime()
    {
        int randomText = Random.Range(0, suggestions.Length);
        textVisualizer.WriteTitle(suggestions[randomText]);
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Start loading the main scene
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // Update the progress bar
        while (!asyncLoad.isDone)
        {
            progressBar.value = asyncLoad.progress;

            // Check if the load has finished
            if (asyncLoad.progress >= 0.9f)
            {
                progressBar.value = 1f;
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
