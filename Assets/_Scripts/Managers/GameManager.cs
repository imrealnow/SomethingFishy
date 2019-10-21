using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public SharedFloat distanceNeeded;
    public SharedString currentScene;
    public SharedGameObject scrollManagerObject;
    public SharedEvent fishyDied;

    [Space]

    public UnityEvent OnLevelCompleted;

    [Space]

    public SharedFloat timeLimit;

    private ScrollManager scrollManager;

    private float distanceTravelled;
    private bool isGameRunning = true;

    public float LevelProgress
    {
        get
        {
            return distanceTravelled / distanceNeeded.Value;
        }
    }

    void Start()
    {
        distanceTravelled = 0;
    }

    void FixedUpdate()
    {
        if (isGameRunning)
        {
            if (timeLimit.Value <= 0)
            {
                fishyDied.Fire();
                isGameRunning = false;
            }
            if (scrollManager == null)
            {
                if (!UpdateScrollManager())
                    return;
            }

            if (LevelProgress >= 1 && OnLevelCompleted != null)
            {
                OnLevelCompleted.Invoke();
                isGameRunning = false;
            }

            if (currentScene.Value == "ThirdLevel")
            {
                if (timeLimit != null)
                    timeLimit.Value -= Time.fixedDeltaTime;
                distanceTravelled += scrollManager.scrollSpeed * Time.fixedDeltaTime * Mathf.Max(1 - LevelProgress, 0.2f);
            }
            else
            {
                distanceTravelled += scrollManager.scrollSpeed * Time.fixedDeltaTime;
            }

        }
    }

    private bool UpdateScrollManager()
    {
        if (scrollManagerObject == null)
        {
            Debug.LogError("Scroll Manager Object not set on GameManager");
            return false;
        }

        scrollManager = scrollManagerObject.Value.GetComponent<ScrollManager>();
        return scrollManager != null;
    }
}
