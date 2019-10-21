using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Identifier/Pickup", order = 1)]
public class PickupIdentifier : ScriptableObject
{
    public string pickupName = "New PickupIdentifier";
    [TextArea]
    public string pickupDescription = "Write a description of the pickup's intended effect here";
}
