using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text seconds, milliseconds;
    public SFloat timeValue;

    private void OnEnable()
    {
        if (timeValue != null)
            timeValue.variableChanged += UpdateTimer;
    }

    private void OnDisable()
    {
        if (timeValue != null)
            timeValue.variableChanged -= UpdateTimer;
    }

    private void UpdateTimer()
    {
        if (timeValue == null)
            return;

        string[] timeAmounts = Mathf.Abs(timeValue.Value).ToString().Split('.');
        seconds.text = timeAmounts[0];
        if (timeAmounts.Length == 1)
            milliseconds.text = "00";
        else
            milliseconds.text = timeAmounts[1];
    }
}
