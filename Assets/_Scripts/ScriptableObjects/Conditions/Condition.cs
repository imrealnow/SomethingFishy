using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : ScriptableObject
{
    /// <summary>
    /// Checks a list of conditions to see if all conditions are met
    /// </summary>
    /// <param name="conditions">list of conditions</param>
    /// <returns>The result of the check</returns>
    public static bool CheckList(List<Condition> conditions)
    {
        if (conditions.Count == 0)
            return false;

        bool result = true;
        for (int i = 0; i < conditions.Count; i++)
        {
            result = result && conditions[i].Check();
        }

        return result;
    }

    public abstract bool Check();
    public virtual void Reset() { }

}
