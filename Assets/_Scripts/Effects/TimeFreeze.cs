using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreeze : MonoBehaviour
{
    private float startTimescale;
    private Coroutine timeFreezer;

    private void Start()
    {
        startTimescale = Time.timeScale;
    }

    public void FreezeTime(float duration)
    {
        timeFreezer = StartCoroutine(TimeFreezer(duration));
    }

    private IEnumerator TimeFreezer(float duration)
    {
        float endTime = Time.realtimeSinceStartup + duration;
        Time.timeScale = 0;

        while (Time.realtimeSinceStartup < endTime)
            yield return null;

        Time.timeScale = startTimescale;
    }
}
