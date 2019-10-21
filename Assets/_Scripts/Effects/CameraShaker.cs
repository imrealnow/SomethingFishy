using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{

    public float duration;
    public float strength;
    public float frequency;
    public float frequencyVariance;

    private Vector2 shakeOffset = new Vector2();
    private Vector3 cameraStartPos;
    private void Start()
    {
        cameraStartPos = Camera.main.transform.position;
    }

    public void ShakeCamera()
    { 
        StartCoroutine(Shaker(duration, strength, frequency));
    }

    IEnumerator Shaker(float duration, float strength, float frequency)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            if(Random.Range(0f,1f) > 0.5f)
                shakeOffset.y = -(SmoothRandom.GetVector3(20).normalized * ((Time.time / endTime) * strength)).y;
            else
                shakeOffset.y = (SmoothRandom.GetVector3(20).normalized * ((Time.time / endTime) * strength)).y;

            transform.position = cameraStartPos + (Vector3)shakeOffset;

            yield return new WaitForSeconds(Random.Range(frequency-frequencyVariance,frequency+frequencyVariance));
        }

        transform.position = cameraStartPos;
        yield return null;
    }
}
