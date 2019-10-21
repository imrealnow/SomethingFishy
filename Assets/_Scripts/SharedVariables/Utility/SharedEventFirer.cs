using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedEventFirer : MonoBehaviour
{
    public void FireEvent(SharedEvent sharedEvent)
    {
        sharedEvent.Fire();
    }
}
