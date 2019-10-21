using UnityEngine;

public class SnapToPixelGrid : MonoBehaviour
{
    [SerializeField]
    private int pixelsPerUnit = 32;

    private void Start()
    {;
    }

    /// <summary>
    /// Snap the object to the pixel grid determined by the given pixelsPerUnit.
    /// Using the parent's world position, this moves to the nearest pixel grid location by 
    /// offseting this GameObject by the difference between the parent position and pixel grid.
    /// </summary>
    private void LateUpdate()
    {
        Vector3 newLocalPosition = Vector3.zero;

        newLocalPosition.x = Mathf.Round(transform.position.x * pixelsPerUnit) / pixelsPerUnit;
        newLocalPosition.y = Mathf.Round(transform.position.y * pixelsPerUnit) / pixelsPerUnit;

        transform.localPosition = newLocalPosition;
    }
}