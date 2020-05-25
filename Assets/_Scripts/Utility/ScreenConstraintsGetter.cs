using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenConstraintsGetter : MonoBehaviour
{
    public SFloat screenMinX, screenMaxX, screenMinY, screenMaxY;
    [Space]
    public SFloat screenWorldHeight, screenWorldWidth;

    private Camera _camera;

    void Awake()
    {
        SetValues();
        Invoke("SetValues", 0.15f);
    }

    private void SetValues()
    {
        _camera = GetComponent<Camera>();

        if (screenMinX != null)
            screenMinX.Value = _camera.ViewportToWorldPoint(Vector3.zero).x;
        if (screenMaxX != null)
            screenMaxX.Value = _camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        if (screenMinY != null)
            screenMinY.Value = _camera.ViewportToWorldPoint(Vector3.zero).y;
        if (screenMaxY != null)
            screenMaxY.Value = _camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        if (screenWorldHeight != null)
        {
            screenWorldHeight.Value = Mathf.Abs
                (
                    _camera.ScreenToWorldPoint(Vector3.zero).y -
                    _camera.ScreenToWorldPoint(new Vector3(0, _camera.pixelHeight, 0)).y
                );
        }
        if (screenWorldWidth != null)
        {
            screenWorldWidth.Value = Mathf.Abs
                (
                    _camera.ScreenToWorldPoint(Vector3.zero).x -
                    _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, 0, 0)).x
                );
        }
    }
}
