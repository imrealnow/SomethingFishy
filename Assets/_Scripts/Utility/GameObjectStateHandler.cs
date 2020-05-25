using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectStateHandler : MonoBehaviour
{
    public GameObjectStateManager stateManager;
    public IntReference stateID;
    public bool setAsDefault;

    private void Awake()
    {
        SceneManager.sceneLoaded += RegisterSelf;
        SceneManager.sceneUnloaded += UnregisterSelf;
    }

    private void OnDestroy()
    {
        UnregisterSelf(SceneManager.GetActiveScene());
    }

    private void RegisterSelf<Scene>(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= RegisterSelf;
        stateID.Value = stateManager.RegisterGameobject(gameObject, setAsDefault);
    }

    private void UnregisterSelf<Scene>(Scene scene)
    {
        SceneManager.sceneUnloaded -= UnregisterSelf;
        stateID.Value = -1;
        stateManager.UnregisterGameobject(gameObject);
    }
}
