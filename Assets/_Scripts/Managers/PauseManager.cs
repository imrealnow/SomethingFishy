using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PauseManager", menuName = "SO/Managers/PauseManager", order = 1)]
public class PauseManager : SManager
{
    public RunningSet pauseObjects;
    [Space]
    public UnityEvent OnPaused;
    public UnityEvent OnUnpaused;

    public bool isPaused = false;

    public override void OnEnabled()
    {
        isPaused = false;
    }

    public override void OnDisabled()
    {
        isPaused = false;
    }

    public void TogglePause()
    {
        SetIsPaused(!isPaused);
    }

    public void SetIsPaused(bool paused)
    {
        if (isPaused == paused)
            return;

        if (pauseObjects.GetSet() != null)
        {
            if (pauseObjects.GetSet().Count > 0)
            {
                foreach (GameObject go in pauseObjects.GetSet())
                {
                    go.SetActive(!paused);
                }
            }
        }

        Time.timeScale = paused ? 0f : 1f;

        isPaused = paused;

        if (isPaused && OnPaused != null)
            OnPaused.Invoke();
        if (!isPaused && OnUnpaused != null)
            OnUnpaused.Invoke();
    }
}
