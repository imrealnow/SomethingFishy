using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public float bobbingAmplitude = 1;
    public float bobbingFrequency = 1;

    public float offsetVariance = 0.5f;
    public float frequencyVariance = 0.1f;
    public bool stepPosition;
    public float stepValue;

    private float bobOffset;
    private float timeOffset;
    private Vector3 startPosition;


    void Start()
    {
        timeOffset = Random.Range(-offsetVariance, offsetVariance);
        startPosition = transform.position;
        bobbingFrequency = bobbingFrequency + Random.Range(-frequencyVariance, frequencyVariance);
    }

    void FixedUpdate()
    {
        bobOffset = bobbingAmplitude * Mathf.Sin((Time.time + timeOffset) * bobbingFrequency);
        transform.position = new Vector3(transform.position.x, startPosition.y + bobOffset);
    }
}
