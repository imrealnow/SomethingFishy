using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTranslator : MonoBehaviour
{
    public SFloat distanceTravelled;
    public SFloat translatedValue;

    public float worldDistanceScale = 0.3f;

    private void OnEnable()
    {
        distanceTravelled.variableChanged += UpdateValue;
    }

    private void OnDisable()
    {
        distanceTravelled.variableChanged -= UpdateValue;
    }

    private void UpdateValue()
    {
        translatedValue.Value = TranslateDistance(distanceTravelled.Value);
    }

    public float TranslateDistance(float distance)
    {
        return Mathf.Round((Mathf.Abs(distance) * worldDistanceScale) * 100) / 100;
    }
}
