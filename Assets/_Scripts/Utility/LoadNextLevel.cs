using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public SharedString nextLevel;
    public string persistentLevel;

    public void LoadLevel()
    {
        if (nextLevel.Value == "" || nextLevel == null)
            return;

        SceneManager.LoadScene(nextLevel.Value,LoadSceneMode.Single);
        SceneManager.LoadScene(persistentLevel, LoadSceneMode.Additive);
    }
}
