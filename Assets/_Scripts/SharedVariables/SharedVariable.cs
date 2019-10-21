using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedVariable<T> : ScriptableObject
{
    [SerializeField]
    private T _variable;

    public delegate void OnVariableChanged();
    public OnVariableChanged variableChanged;

    public T Value {
        get { return _variable; }
        set {
            _variable = value;
            if (variableChanged != null)
                variableChanged.Invoke();
        }
    }
}

