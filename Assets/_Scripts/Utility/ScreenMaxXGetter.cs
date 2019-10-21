using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMaxXGetter : MonoBehaviour
{
    public SharedFloat holderVariable;

    void Start()
    {
        holderVariable.Value = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0)).x;
    }
}
