using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNameGetter : MonoBehaviour
{
    public SharedString sharedString;

    void Start()
    {
        if (sharedString != null)
            sharedString.Value = SceneManager.GetActiveScene().name;
    }
}
