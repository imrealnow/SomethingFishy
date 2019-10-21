using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    public int value;
    public GameObject leftHalf, rightHalf;

    public void ChangeValue(int newValue)
    {
        if (leftHalf == null && rightHalf == null)
        {
            Debug.LogError("Half halfs not set correctly");
            return;
        }

        value = Mathf.Clamp(newValue, 0, 2);

        leftHalf.SetActive(value >= 1);
        rightHalf.SetActive(value >= 2);
    }
}
