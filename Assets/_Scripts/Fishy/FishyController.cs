using UnityEngine;

public class FishyController : MonoBehaviour
{
    public Vector2 baseSpeeds;
    public Vector2 maxSpeeds;
    public float drag = 0.99f;

    public Vector2 yBounds;

    private ScrollManager scrollManager;
    private SpriteRenderer spriteRenderer;
    private Bobber bobber;

    private float xVel = 0f;
    private float yVel = 0f;

    private bool hasControl = true;
    private bool hasWon = false;

    void Start()
    {
        scrollManager = FindObjectOfType<ScrollManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bobber = GetComponent<Bobber>();
    }

    void FixedUpdate()
    {
        if (hasControl)
        {
            ControlFishy();
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

        if (transform.position.y > yBounds.y && yVel > 0)
            yVel *= 0.9f;
        if (transform.position.y < yBounds.x && yVel < 0)
            yVel *= 0.9f;

        xVel *= drag;
        yVel *= drag;
    }

    private void ControlFishy()
    {
        float angle = Mathf.Atan2(scrollManager.scrollSpeed * Time.smoothDeltaTime * 2, yVel * Time.smoothDeltaTime) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

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

        scrollManager.ScrollSpeed += xVel * Time.smoothDeltaTime;
    }

    private float CalculateYVel(float currentYVel, float delta)
    {
        if (Mathf.Sign(currentYVel) != Mathf.Sign(delta))
            delta *= 2.5f;

        return currentYVel + delta;
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
}
