using UnityEngine;

public abstract class Positioner : MonoBehaviour
{
    public FloatReference value;
    public float offset;
    public bool setOnStart, setOnUpdate, setOnVariableChanged;

    private void OnEnable()
    {
        if (setOnVariableChanged)
            value._variable.variableChanged += Reposition;
    }

    private void OnDisable()
    {
        if (setOnVariableChanged)
            value._variable.variableChanged -= Reposition;
    }

    private void Start()
    {
        if (setOnStart)
            Reposition();
    }

    private void Update()
    {
        if (setOnUpdate)
            Reposition();
    }

    protected abstract void Reposition();
}
