using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingScroller : ParallaxScroller
{
    public float risingSpeed;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void SetPosition(float newX)
    {
        transform.position = new Vector3(newX, transform.position.y + risingSpeed * Time.deltaTime);
    }
}
