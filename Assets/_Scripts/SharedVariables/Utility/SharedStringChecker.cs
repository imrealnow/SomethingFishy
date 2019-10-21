using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SharedStringChecker : MonoBehaviour
{
    public string valueToCheckFor;
    public SharedString sharedString;
    public UnityEvent whenValuesMatch;

    private void OnEnable()
    {
        if (sharedString != null)
            sharedString.variableChanged += CheckValues;
    }

    private void OnDisable()
    {
        if (sharedString != null)
            sharedString.variableChanged -= CheckValues;
    }

    void Start()
    {
        CheckValues();
    }

    private void CheckValues()
    {
        if(sharedString != null && whenValuesMatch != null)
        {
            if (sharedString.Value == valueToCheckFor)
                whenValuesMatch.Invoke();
        }
    }
}
