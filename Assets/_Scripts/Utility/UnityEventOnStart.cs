using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventOnStart : MonoBehaviour
{
    public UnityEvent runOnStart;

    void Awake()
    {
        if (runOnStart != null)
            runOnStart.Invoke();
    }
}
