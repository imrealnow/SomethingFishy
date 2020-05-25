using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishyController : MonoBehaviour
{
    public Vector2 yBounds;
    public Vector2 baseSpeeds;
    public Vector2 maxSpeeds;
    public float drag = 0.99f;

    [Space]
    public float touchDistanceThreshold = 0.5f;

    [Space]
    public SGameObject cameraReference;
    public ScrollManager scrollManager;

    [Space]
    public UnityEvent OnDirectionChanged;

    private SpriteRenderer spriteRenderer;
    private Bobber bobber;
    private Camera mainCamera;

    public Vector2 Velocity { get { return new Vector2(xVel, yVel); } }

    private float xVel = 0f;
    private float yVel = 0f;

    private Vector2 firstTouchPosition;

    private bool hasControl = true;
    private bool hasWon = false;
    private bool aboveBound = false;
    private bool belowBound = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bobber = GetComponent<Bobber>();
        mainCamera = cameraReference.Value.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if (hasControl)
        {
            ApplySwimmingRotation();
            ControlFishy();
            scrollManager.ScrollSpeed += xVel * Time.smoothDeltaTime;
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.zero), Time.smoothDeltaTime);
        }

        transform.position = new Vector3
            (
                hasWon ? transform.position.x + 2 * Time.smoothDeltaTime : transform.position.x,
                transform.position.y + yVel * Time.deltaTime,
                0
            );

        aboveBound = transform.position.y > yBounds.y;
        belowBound = transform.position.y < yBounds.x;

        if (yVel > 0 && aboveBound)
            yVel *= 0.9f;
        if (yVel < 0 && belowBound)
            yVel *= 0.9f;

        xVel *= drag;
        yVel *= drag;
    }

    protected virtual void ControlFishy()
    {
#if UNITY_WEBGL
        if (Input.GetKey(KeyCode.W) && transform.position.y < yBounds.y)
        {
            float delta = baseSpeeds.y * scrollManager.GetSpeedPercentage();
            yVel = Mathf.Min(CalculateYVel(yVel, delta), maxSpeeds.y);
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y > yBounds.x)
        {
            float delta = -baseSpeeds.y * scrollManager.GetSpeedPercentage();
            yVel = Mathf.Max(CalculateYVel(yVel, delta), -maxSpeeds.y);
        }
        if (Input.GetKey(KeyCode.A))
        {
            xVel = Mathf.Min(xVel + baseSpeeds.x, maxSpeeds.x);
        }
        if (Input.GetKey(KeyCode.D))
        {
            xVel = Mathf.Max(xVel - baseSpeeds.x, -maxSpeeds.x);
        }
#endif
#if UNITY_ANDROID
        List<Touch> touches = InputHelper.GetTouches();

        if (touches.Count > 0)
        {
            Vector3 touchWorldPosition = mainCamera.ScreenToWorldPoint(touches[0].position);
            touchWorldPosition.z = transform.position.z;
            float distanceToTouch = Mathf.Abs(touchWorldPosition.y - transform.position.y);

            // up and down
            float yVelDelta = touches[0].deltaPosition.y / 20f * scrollManager.GetSpeedPercentage();

            //if out out bounds, don't let fishy move more out of bounds
            if (aboveBound)
                yVelDelta = Mathf.Min(0, yVelDelta);
            if (belowBound)
                yVelDelta = Mathf.Max(0, yVelDelta);

            yVel = CalculateYVel(yVel, yVelDelta);

            //// left and right
            //if (touches[0].phase == TouchPhase.Began)
            //    firstTouchPosition = touchWorldPosition;
            //else
            //{
            //    float xVelScale = Mathf.Clamp(firstTouchPosition.x - touchWorldPosition.x, -touchDistanceThreshold, touchDistanceThreshold);
            //    xVel = Mathf.Clamp(maxSpeeds.x * (xVelScale / touchDistanceThreshold), -maxSpeeds.x, maxSpeeds.x);
            //}
        }
#endif
    }

    private float CalculateYVel(float currentYVel, float delta)
    {
        if ((currentYVel < 0 && delta > 0) || (currentYVel > 0 && delta < 0))
        {
            delta *= 2.5f;
            if (OnDirectionChanged != null)
                OnDirectionChanged.Invoke();
        }

        return Mathf.Clamp(currentYVel + delta, -maxSpeeds.y, maxSpeeds.y);
    }

    private void ApplySwimmingRotation()
    {
        float angle = Mathf.Atan2(scrollManager.scrollSpeed * Time.smoothDeltaTime * 2, yVel * Time.smoothDeltaTime) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Win()
    {
        hasControl = false;
        hasWon = true;
    }

    public void Die()
    {
        hasControl = false;
        spriteRenderer.flipY = true;
        bobber.enabled = true;
    }

    public void SetXVelocity(float velocity)
    {
        xVel = velocity;
    }
    public void SetYVelocity(float velocity)
    {
        yVel = velocity;
    }
}
