using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollVelocityGetter : MonoBehaviour
{
    public SharedFloat scrollVelocity;
    private ScrollManager scrollManager;

    private void Start()
    {
        scrollManager = GetComponent<ScrollManager>();
    }

    private void FixedUpdate()
    {
        scrollVelocity.Value = scrollManager.ScrollSpeed;
    }
}
