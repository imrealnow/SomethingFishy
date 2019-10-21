using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedFloatSetter : MonoBehaviour
{
    public SharedFloat sharedFloat;
    public float value;
    public bool setOnStart;

    void Start()
    {
        if (sharedFloat != null && setOnStart)
            sharedFloat.Value = value;
    }

    public void SetSharedFloatValue(float value)
    {
        if (sharedFloat != null)
            sharedFloat.Value = value;
    }
}
