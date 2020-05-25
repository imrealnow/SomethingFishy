using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEventStateSwitcher : MonoBehaviour
{
    public GameObjectStateManager stateManager;
    public List<ESPair> eventStatePairs = new List<ESPair>();

    void OnEnable()
    {
        if (eventStatePairs.Count > 0)
        {
            for (int i = 0; i < eventStatePairs.Count; i++)
            {
                if (eventStatePairs[i] != null)
                {
                    eventStatePairs[i].stateManager = stateManager;
                    eventStatePairs[i].sharedEvent.sharedEvent += eventStatePairs[i].switchFunction;
                }
            }
        }
    }

    void OnDisable()
    {
        if (eventStatePairs.Count > 0)
        {
            for (int i = 0; i < eventStatePairs.Count; i++)
            {
                if (eventStatePairs[i] != null)
                {
                    eventStatePairs[i].sharedEvent.sharedEvent -= eventStatePairs[i].switchFunction;
                }
            }
        }
    }
}

[Serializable]
public class ESPair
{
    public SEvent sharedEvent;
    public IntReference stateID;
    [HideInInspector] public GameObjectStateManager stateManager;
    public Action switchFunction;

    public ESPair()
    {
        switchFunction = () => { stateManager.SwitchState(stateID.Value); };
    }

}