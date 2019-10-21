using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "Shared Variables/Event", order = 1)]
public class SharedEvent : ScriptableObject
{
    public Action sharedEvent;

    public void Fire()
    {
        if (sharedEvent != null)
            sharedEvent.Invoke();
    }
}