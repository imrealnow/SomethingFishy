using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentDisabler : MonoBehaviour
{
    public List<MonoBehaviour> componentsToDisable = new List<MonoBehaviour>();
    private bool componentsDisabled = false;
    
    public void SetComponentsEnabled(bool enabled)
    {
        if (componentsToDisable.Count == 0 || componentsDisabled == !enabled)
            return;
        
        for(int i = 0; i < componentsToDisable.Count; i++)
        {
            componentsToDisable[i].enabled = enabled;
        }

        componentsDisabled = enabled;
    }
}
