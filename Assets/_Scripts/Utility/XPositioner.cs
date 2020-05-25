using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPositioner : Positioner
{
    protected override void Reposition()
    {
        transform.position = new Vector3(value.Value + offset, transform.position.y, transform.position.z);
    }
}
