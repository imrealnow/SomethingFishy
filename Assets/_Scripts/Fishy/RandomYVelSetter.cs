using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomYVelSetter : MonoBehaviour
{
    public float amount;
    
    private FishyController fishyController;

    private void Start()
    {
        fishyController = GetComponent<FishyController>();
    }

    public void RandomlySetyVel()
    {
        float randomYVel = Random.Range(-amount, amount);
        fishyController.SetYVelocity(fishyController.Velocity.y + randomYVel);
    }
}
