using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void RestartCurrentScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.UnloadSceneAsync(scene);
        SceneManager.LoadScene(scene.name, LoadSceneMode.Additive);
    }
}
