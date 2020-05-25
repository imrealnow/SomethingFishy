using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YPositioner : Positioner
{
    protected override void Reposition()
    {
        transform.position = new Vector3( transform.position.x, value.Value + offset, transform.position.z);
    }
}
