using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomZRotator : MonoBehaviour, IPoolable
{
    public FloatReference minZRotation, maxZRotation;

    void Start()
    {
        RotateObject();
    }

    public void Reuse()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        float randomZRotation = Random.Range(minZRotation.Value, maxZRotation.Value);
        transform.rotation = Quaternion.Euler(0, 0, randomZRotation);
    }
}
