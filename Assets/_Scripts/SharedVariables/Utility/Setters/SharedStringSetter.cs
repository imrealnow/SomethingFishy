using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedStringSetter : MonoBehaviour
{
    public SharedString sharedString;
    public string value;
    public bool setOnStart = true;

    void Start()
    {
        if (sharedString != null && setOnStart)
            sharedString.Value = value;
    }

    public void SetValue(string value)
    {
        if (sharedString != null)
            sharedString.Value = value;
    }
}
