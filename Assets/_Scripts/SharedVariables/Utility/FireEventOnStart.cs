using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEventOnStart : MonoBehaviour
{
    public SharedEvent sharedEvent;

    void Start()
    {
        if (sharedEvent != null)
            sharedEvent.Fire();
    }
}
