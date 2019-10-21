using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingScroller : ParallaxScroller
{
    public float bobbingAmplitude = 1;
    public float bobbingFrequency = 1;

    public float offsetVariance = 0.5f;
    public float frequencyVariance = 0.1f;

    private float bobOffset;
    private float timeOffset;

    void Start()
    {
        timeOffset = Random.Range(-offsetVariance, offsetVariance);
        bobbingFrequency = bobbingFrequency + Random.Range(-frequencyVariance, frequencyVariance);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        bobOffset = bobbingAmplitude * Mathf.Sin((Time.time + timeOffset) * bobbingFrequency);
    }

    protected override void SetPosition(float newX)
    {
        transform.position = new Vector3(newX, startPosition.y + bobOffset);
    }
}
