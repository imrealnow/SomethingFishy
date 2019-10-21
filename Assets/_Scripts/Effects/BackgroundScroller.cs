using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(0, 1)]
    public float distanceFromCamera; // 1 = infinity
    public float scrollPos;

    public float elementsWidth;

    public Transform firstElement, secondElement;

    private ScrollManager scrollManager;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    private float elementWidth;
    private float screenWidth;

    private bool firstVisible = true;

    private float firstStartXPos;

    protected float scrollAmount = 0f;

    void Start()
    {
        scrollManager = FindObjectOfType<ScrollManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = FindObjectOfType<Camera>();

        elementWidth = elementsWidth;

        firstStartXPos = firstElement.position.x;
        float screenMinX = mainCamera.ScreenToWorldPoint(Vector3.zero).x;
        float screenMaxX = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0, 0)).x;
        screenWidth = Mathf.Abs(screenMinX - screenMaxX);
    }

    void FixedUpdate()
    {
        scrollAmount += scrollManager.scrollSpeed * Time.fixedDeltaTime;

        float scaledPosition = firstStartXPos + (scrollAmount * (1 - distanceFromCamera));
        scaledPosition = scaledPosition % elementWidth;

        firstElement.localPosition = new Vector3(
                                         firstVisible ? scaledPosition : elementWidth + scaledPosition,
                                         0f
                                        );
        secondElement.localPosition = new Vector3(
                                         firstVisible ? elementWidth + scaledPosition : scaledPosition,
                                         0f
                                        );

        if (firstVisible && firstElement.position.x < mainCamera.transform.position.x - screenWidth / 2 - elementWidth / 2)
            firstVisible = false;
        else if (!firstVisible && secondElement.position.x < mainCamera.transform.position.x - screenWidth / 2 - elementWidth / 2)
            firstVisible = true;
    }
}
