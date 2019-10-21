using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public AudioMixerSnapshot mainSnapShot;

    public void Load(string sceneName)
    {
        Time.timeScale = 1f;
        if (mainSnapShot != null)
            mainSnapShot.TransitionTo(0f);

        SceneManager.LoadScene(sceneName);
    }
}
