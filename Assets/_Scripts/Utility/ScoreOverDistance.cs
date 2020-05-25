using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOverDistance : MonoBehaviour
{
    public ScoreManager scoreManager;
    public SFloat distanceTravelled;
    public int scorePerDistanceUnit;

    private float lastDistanceTravelled;

    private void OnEnable()
    {
        lastDistanceTravelled = Mathf.Abs(distanceTravelled.Value);
        distanceTravelled.variableChanged += AddDistanceScore;
    }

    private void OnDisable()
    {
        distanceTravelled.variableChanged -= AddDistanceScore;
    }

    private void AddDistanceScore()
    {
        int difference = Mathf.Abs(Mathf.RoundToInt(distanceTravelled.Value)) - Mathf.RoundToInt(lastDistanceTravelled);
        if (difference > 0)
            scoreManager.ChangeScore(difference * scorePerDistanceUnit);
        lastDistanceTravelled = Mathf.Abs(distanceTravelled.Value);
    }
}
