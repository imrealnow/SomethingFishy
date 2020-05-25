using UnityEngine;

public class XMover : MonoBehaviour
{
    public ScrollManager scrollManager;
    public float minX;
    public float maxX;

    private float xRange;
    private float startSpeedPercentage;
    private void Start()
    {
        xRange = maxX - minX;
        startSpeedPercentage = scrollManager.GetSpeedPercentage();
    }

    private void FixedUpdate()
    {
        float xPosition = minX + xRange * scrollManager.GetSpeedPercentage();
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
    }
}
