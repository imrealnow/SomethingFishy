using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Vector3Reference objectPosition;
    public AnimationCurve sizeScaler, transparencyScaler;
    public float maxDistance;
    public float xOffset;
    public float yLevel;
    public float zLevel;
    public bool repositionX;

    private SpriteRenderer spriteRenderer;
    private Color startColor;
    private Vector3 startScale;

    private float yRange;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        startScale = transform.localScale;
        yRange = Mathf.Abs(yLevel - maxDistance);
    }

    void Update()
    {
        float xPos = repositionX ? objectPosition.Value.x : transform.position.x;
        float distanceScale = (yLevel - objectPosition.Value.y  + yRange) / (maxDistance + yRange);
        transform.position = new Vector3(xPos + xOffset, yLevel, zLevel);
        transform.localScale = startScale * sizeScaler.Evaluate(distanceScale);
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a * transparencyScaler.Evaluate(distanceScale));
    }
}
