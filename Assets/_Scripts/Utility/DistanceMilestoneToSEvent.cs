using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DistanceMilestoneToSEvent : MonoBehaviour
{
    public SFloat translatedDistance;
    public List<DistanceEventPair> distanceEventPairs = new List<DistanceEventPair>();

    private void OnEnable()
    {
        translatedDistance.variableChanged += CheckPairs;
    }

    private void OnDisable()
    {
        translatedDistance.variableChanged -= CheckPairs;
    }

    private void CheckPairs()
    {
        for (int i = 0; i < distanceEventPairs.Count; i++)
        {
            if (distanceEventPairs[i].distance <= translatedDistance.Value
                && distanceEventPairs[i].whenDistanceReached != null
                && !distanceEventPairs[i].distanceReached)
            {
                distanceEventPairs[i].whenDistanceReached.Fire();
                distanceEventPairs[i].distanceReached = true;
            }
        }
    }
}

[Serializable]
public class DistanceEventPair
{
    public float distance;
    public SEvent whenDistanceReached;
    [HideInInspector] public bool distanceReached = false;
}