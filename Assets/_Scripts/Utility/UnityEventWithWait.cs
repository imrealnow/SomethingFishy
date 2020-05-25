using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventWithWait : MonoBehaviour
{
    public UnityEvent unityEvent;

    public void RunEvent(float duration)
    {
        StartCoroutine(InvokeEvent(duration));
    }

    private IEnumerator InvokeEvent(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        if (unityEvent != null)
            unityEvent.Invoke();
    }
}
