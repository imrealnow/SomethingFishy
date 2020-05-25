using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectStateManager", menuName = "SO/Managers/GameObjectStateManager", order = 1)]
public class GameObjectStateManager : SManager
{
    public List<GameObject> gameObjectStates = new List<GameObject>();
    public GameObject defaultState;
    
    private GameObject currentState;

    public override void OnEnabled()
    {
        VerifyState();
    }

    private void VerifyState()
    {
        if (gameObjectStates.Count == 0) //if the list is empty cancel the verification
            return;

        GameObject activeState = null;
        foreach (GameObject GO in gameObjectStates)
        {
            if (GO == defaultState)
            {
                currentState = GO;
                GO.SetActive(true);
            }
            else
            {
                GO.SetActive(false);
            }
        }

        currentState = activeState;
    }

    public void SwitchState(SInt gameObjectStateID)
    {
        SwitchState(gameObjectStateID.Value);
    }

    public void SwitchState(int gameobjectIndex)
    {
        if (gameobjectIndex - 1 > gameObjectStates.Count || gameobjectIndex < 0 || gameobjectIndex == gameObjectStates.IndexOf(currentState))
            return;

        currentState = gameObjectStates[gameobjectIndex];
        currentState.SetActive(true);

        foreach(GameObject GO in gameObjectStates)
        {
            if (GO == currentState) //skip if it's the current state
                continue;

            GO.SetActive(false);
        }
    }

    public int RegisterGameobject(GameObject obj, bool setAsDefault = false)
    {
        if (!gameObjectStates.Contains(obj))
            gameObjectStates.Add(obj);

        if (setAsDefault)
            defaultState = obj;

        VerifyState();
        return gameObjectStates.IndexOf(obj);
    }

    public void UnregisterGameobject(GameObject obj)
    {
        if (gameObjectStates.Contains(obj))
            gameObjectStates.Remove(obj);

        VerifyState();
    }
}
