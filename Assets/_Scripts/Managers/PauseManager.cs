using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    public SharedGameObjectList pauseObjects;
    [Space]
    public UnityEvent OnPaused;
    public UnityEvent OnUnpaused;

    private bool isPaused = false;
    private bool isLocked = false;

    public void TogglePause(bool lockPause = false)
    {
        if (!lockPause && isLocked) // it's locked and you're not unlocking it
            return;

        isLocked = lockPause && !isLocked;

        SetIsPaused(!isPaused);
    }

    public void SetIsPaused(bool paused)
    {
        if (isPaused == paused || isLocked)
            return;

        if (pauseObjects.Value.Count > 0)
        {
            foreach (GameObject go in pauseObjects.Value)
            {
                go.SetActive(!paused);
            }
        }

        Time.timeScale = paused ? 0f : 1f;

        isPaused = paused;

        if (isPaused && OnPaused != null)
            OnPaused.Invoke();
        if (!isPaused && OnUnpaused != null)
            OnUnpaused.Invoke();
    }

    public void SetPauseLocking(bool locked)
    {
        isLocked = locked;
    }
}
