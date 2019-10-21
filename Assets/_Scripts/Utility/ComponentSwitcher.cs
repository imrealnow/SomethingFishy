using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentSwitcher : MonoBehaviour
{
    public MonoBehaviour firstComponent;
    public MonoBehaviour secondComponent;

    void Start()
    {
        if (firstComponent.enabled)
            secondComponent.enabled = false;
    }

    public void SwitchComponents()
    {
        if(firstComponent.enabled)
        {
            secondComponent.enabled = true;
            firstComponent.enabled = false;
        }
        else
        {
            firstComponent.enabled = true;
            secondComponent.enabled = false;
        }
    }
}
