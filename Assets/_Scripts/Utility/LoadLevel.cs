using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public List<string> scenesToLoad;

    private Scene currentScene;

    public void LoadScenes()
    {
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            SceneManager.LoadScene(scenesToLoad[i], i == 0 ? LoadSceneMode.Single : LoadSceneMode.Additive);
        }
    }

    public void LoadSingleScene(string scenename)
    {
        SceneManager.LoadScene(scenename, LoadSceneMode.Single);
    }
}
