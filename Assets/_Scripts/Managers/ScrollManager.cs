using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScrollManager", menuName = "SO/Managers/ScrollManager", order = 1)]
public class ScrollManager : SManager
{
    public float scrollSpeed = 1;
    public float minScrollSpeed = -10f;
    public float maxScrollSpeed = -2.5f;

    [Space]
    public SFloat distanceTravelled;

    public float ScrollSpeed { get { return scrollSpeed; } set { scrollSpeed = Mathf.Clamp(value, minScrollSpeed, maxScrollSpeed); } }

    private float baseScrollSpeed;
    private float startScrollSpeed;
    private bool isScrolling;

    public override void OnEnabled()
    {
        isScrolling = true;
        startScrollSpeed = scrollSpeed;
        baseScrollSpeed = scrollSpeed;
    }

    public override void OnDisabled()
    {
        scrollSpeed = startScrollSpeed;
        distanceTravelled.Value = 0;
    }

    public override void Update()
    {
        if (isScrolling)
        {
            scrollSpeed = Mathf.Lerp(scrollSpeed, baseScrollSpeed, Time.fixedDeltaTime);
            distanceTravelled.Value += scrollSpeed * Time.fixedDeltaTime;
        }
        else
            scrollSpeed = Mathf.Lerp(scrollSpeed, 0, Time.fixedDeltaTime);
    }

    public void SetScrolling(bool shouldScroll)
    {
        isScrolling = shouldScroll;
    }

    public float GetSpeedPercentage()
    {
        return Mathf.Abs(scrollSpeed) / Mathf.Abs( scrollSpeed > 0 ? maxScrollSpeed : minScrollSpeed);
    }

    public void SetScrollSpeed(float speed)
    {
        baseScrollSpeed = speed;
    }
}
