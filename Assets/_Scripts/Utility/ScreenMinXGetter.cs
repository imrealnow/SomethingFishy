using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMinXGetter : MonoBehaviour
{
    public SharedFloat holderVariable;
    public SharedGameObject cameraReference;

    void Start()
    {
        holderVariable.Value = cameraReference.Value.GetComponent<Camera>().ScreenToWorldPoint(Vector3.zero).x;
    }
}
