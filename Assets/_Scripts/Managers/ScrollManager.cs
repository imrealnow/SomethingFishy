using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    public float scrollSpeed = 1;
    public float scrollXPos = 0;
    public float minScrollSpeed = -10f;
    public float maxScrollSpeed = -2.5f;
    public float ScrollSpeed { get { return scrollSpeed; } set { scrollSpeed = Mathf.Clamp(value, minScrollSpeed, maxScrollSpeed); } }
    
    private float startScrollSpeed;
    private bool isScrolling = true;

    void Start()
    {
        startScrollSpeed = scrollSpeed;
    }

    void FixedUpdate()
    {
        if(isScrolling)
            scrollSpeed = Mathf.Lerp(scrollSpeed, startScrollSpeed, Time.fixedDeltaTime);
        else
            scrollSpeed = Mathf.Lerp(scrollSpeed, 0, Time.fixedDeltaTime);
    }

    public void SetScrolling(bool shouldScroll)
    {
        isScrolling = shouldScroll;
    }

    public float GetSpeedPercentage()
    {
        return Mathf.Abs(scrollSpeed) / Mathf.Abs(minScrollSpeed);
    }

    public void SetScrollSpeed(float speed)
    {
        startScrollSpeed = speed;
    }
}
