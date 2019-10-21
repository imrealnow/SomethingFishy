using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour, IComparable<ParallaxScroller>, IPoolable
{

    public static List<ParallaxScroller> allScrollingObjects = new List<ParallaxScroller>();
    public int startingSortOrder = 3;

    private static bool objectsSorted = false;

    [Range(0, 1)]
    public float distanceFromCamera; // 1 = infinity
    public bool repeatOnLeaveScreen = true;
    public bool startOffScreen = true;

    protected SpriteRenderer spriteRenderer { get; private set; }

    protected Camera mainCamera;
    protected ScrollManager scrollManager;
    protected Vector3 startPosition;

    protected float screenMinX;
    protected float screenMaxX;
    protected float screenWidth;

    protected float scrollAmount = 0f;
    protected bool initialised = false;

    void Start()
    {
        mainCamera = Camera.main;
        scrollManager = FindObjectOfType<ScrollManager>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void OnEnable()
    {
        if (SetReferences())
        {
            Initialise();
        }
    }

    protected void OnDisable()
    {
        initialised = false;
        if (allScrollingObjects.Contains(this))
            allScrollingObjects.Remove(this);
    }

    protected virtual void FixedUpdate()
    {
        if (!objectsSorted)
            OrderRenderers();

        ScrollObject();
    }

    protected void Initialise()
    {
        screenMinX = mainCamera.ScreenToWorldPoint(Vector3.zero).x;
        screenMaxX = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0, 0)).x;
        screenWidth = Mathf.Abs(screenMinX - screenMaxX);

        if (!allScrollingObjects.Contains(this) && spriteRenderer != null)
            allScrollingObjects.Add(this);

        startPosition = transform.position;
        scrollAmount = -(screenWidth / 2);
        initialised = true;
    }

    protected void ScrollObject()
    {
        if (scrollManager == null || mainCamera == null)
        {
            if (!SetReferences())
                return;
        }

        scrollAmount += scrollManager.scrollSpeed * Time.fixedDeltaTime;

        float elementWidth = spriteRenderer != null ? spriteRenderer.bounds.extents.x * 2 : 4f;
        float scaledPosition = startPosition.x + (scrollAmount * (1 - distanceFromCamera));

        if (!repeatOnLeaveScreen)
        {
            if ((transform.position.x + elementWidth) < screenMinX)
            {
                transform.position = startPosition;
                PoolObject objPool = gameObject.GetComponent<PoolObject>();
                if (objPool != null)
                {
                    objPool.ReturnToPool();
                }
                else
                    Destroy(gameObject);
            }
        }

        scaledPosition = (scaledPosition % (screenWidth + elementWidth * 3));
        if (startOffScreen)
            scaledPosition += (screenMaxX + elementWidth);

        SetPosition(scaledPosition);
    }

    protected virtual void SetPosition(float newX)
    {
        transform.position = new Vector3(newX, transform.position.y);
    }

    protected static void OrderRenderers()
    {
        allScrollingObjects.Sort();
        for (int i = 0; i < allScrollingObjects.Count; i++)
        {
            allScrollingObjects[i].spriteRenderer.sortingOrder = i + allScrollingObjects[i].startingSortOrder;
        }
        objectsSorted = true;
    }

    protected bool SetReferences()
    {
        if (scrollManager != null && mainCamera != null)
            return true;
            mainCamera = Camera.main;
            scrollManager = FindObjectOfType<ScrollManager>();

        if (scrollManager != null && mainCamera != null && !initialised)
            Initialise();

        return scrollManager != null && mainCamera != null;
    }

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

    public void Reuse()
    {
        scrollAmount = 0f;
        startPosition = transform.position;
    }

    public void ResetScrollPosition()
    {
        scrollAmount = -(screenWidth / 2f);
    }
}
