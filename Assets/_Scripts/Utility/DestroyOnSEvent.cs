using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnSEvent : MonoBehaviour
{
    public SEvent sharedEvent;

    private void OnEnable()
    {
        sharedEvent.sharedEvent += DestroySelf;
    }

    private void OnDestroy()
    {
        sharedEvent.sharedEvent -= DestroySelf;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
