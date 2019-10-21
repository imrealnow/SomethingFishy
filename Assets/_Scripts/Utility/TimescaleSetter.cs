using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimescaleSetter : MonoBehaviour
{
    public float _timeScale;

    void Start()
    {
        Time.timeScale = _timeScale;
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
