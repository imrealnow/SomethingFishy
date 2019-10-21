using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIStateManager : MonoBehaviour
{
    public List<GameObject> UIHolders = new List<GameObject>();
    public GameObject defaultState;
    
    private GameObject currentState;

    void Awake()
    {
        VerifyState();
    }

    private void OnEnable()
    {
        VerifyState();
    }

    private void VerifyState()
    {
        if (UIHolders.Count == 0) //if the list is empty cancel the verification
            return;

        GameObject activeState = null;
        foreach (GameObject GO in UIHolders)
        {
            if (GO.activeInHierarchy && activeState == null || GO == currentState) //if this uiholder is active, and it hasn't been set yet. or if it's the current state
            {
                activeState = GO;
                GO.SetActive(true);
            }
            else // make sure that it's disabled
            {
                GO.SetActive(false);
            }
        }
        if (activeState == null) //if all uiholders were inactive
        {
            if (defaultState != null) //if there's a defaultState set the active state to it
                activeState = defaultState;
            else // else set it to the first in the list
                activeState = UIHolders[0];
        }

        currentState = activeState;
    }

    public void SwitchState(int uiHolderIndex)
    {
        if (uiHolderIndex - 1 > UIHolders.Count || uiHolderIndex < 0 || uiHolderIndex == UIHolders.IndexOf(currentState))
            return;

        currentState = UIHolders[uiHolderIndex];
        currentState.SetActive(true);

        foreach(GameObject GO in UIHolders)
        {
            if (GO == currentState) //skip if it's the current state
                continue;

            GO.SetActive(false);
        }
    }
}
