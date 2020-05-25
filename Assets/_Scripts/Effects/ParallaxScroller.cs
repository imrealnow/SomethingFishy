using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour, IComparable<ParallaxScroller>, IPoolable
{
    public FloatReference minX;
    public FloatReference maxX;
    public FloatReference elementWidth;

    [Space]
    public SFloat screenWidth;

    [Range(-1, 1)]
    public float distanceFromCamera; // 1 = infinity
    public bool repeatOnLeaveScreen = true;
    public bool startOffScreen = true;

    public ScrollManager scrollManager;
    protected float startScrollAmount;
    protected Vector3 startPosition;

    protected float scrollAmount = 0f;
    protected float distanceToMinX { get { return transform.position.x - minX.Value; } }

    void OnEnable()
    {
        if (startOffScreen)
            scrollAmount = screenWidth.Value;
        else
            scrollAmount = distanceToMinX;

        startScrollAmount = scrollAmount;
        startPosition = transform.position;
    }

    protected virtual void FixedUpdate()
    {
        ScrollObject();
    }

    protected void ScrollObject()
    {
        // if the camera isn't "moving", don't bother calculating everything else (don't do anything)
        if (scrollManager.scrollSpeed == 0)
            return;

        // check the scroll manager's speed to determine the movement direction
        bool goingRight = scrollManager.scrollSpeed > 0;

        // add the depth scaled scroll amount to the total scroll amount
        scrollAmount += scrollManager.scrollSpeed * (1 - distanceFromCamera) * Time.fixedDeltaTime;
        float scaledPosition = minX.Value + scrollAmount;
        if (startOffScreen)
            scaledPosition += elementWidth.Value;
        bool offLeftSide = !goingRight && (scaledPosition + elementWidth.Value) < minX.Value;
        bool offRightSide = goingRight && (scaledPosition + elementWidth.Value) > maxX.Value;

        if (offLeftSide || offRightSide)
        {
            // destroy it if it's not being repeated
            if (!repeatOnLeaveScreen)
            {
                //scrollAmount = startScrollAmount;
                PoolObject objPool = gameObject.GetComponent<PoolObject>();
                if (objPool != null)
                {
                    objPool.ReturnToPool();
                }
                else
                    Destroy(gameObject);

                return;
            }
            else
            {
                // reset position to other side
                scrollAmount = goingRight ? 0 - elementWidth.Value : screenWidth.Value + elementWidth.Value;
                scaledPosition = minX.Value + scrollAmount;
            }
        }
        SetPosition(scaledPosition);
    }

    // sets the position of the object to the calculated position
    protected virtual void SetPosition(float newX)
    {
        transform.position = new Vector3(newX, transform.position.y);
    }

    // used to compare depth of object to other object for sorting purposes
    public int CompareTo(ParallaxScroller obj)
    {
        if (obj == null) return 1;

        if (obj.distanceFromCamera < this.distanceFromCamera)
            return -1;
        else if (obj.distanceFromCamera == this.distanceFromCamera)
            return 0;
        else
            return 1;
    }

    public void ResetScrollPosition()
    {
        scrollAmount = startScrollAmount;
    }

    // reset all the variables that need to be reset when the object is taken from the pool and reused
    public void Reuse()
    {
        startPosition = transform.position;
        scrollAmount = startOffScreen ? screenWidth.Value : distanceToMinX;
        startScrollAmount = scrollAmount;
    }
}
