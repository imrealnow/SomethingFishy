using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntEquals", menuName = "SO/Conditions/IntEquals", order = 1)]
public class IntEqualsCondition : Condition
{
    public IntReference intA, intB;

    public override bool Check()
    {
        return intA.Value == intB.Value;
    }
}
