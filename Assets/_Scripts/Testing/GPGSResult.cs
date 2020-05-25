using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPGSResult : MonoBehaviour
{
    public GPGSManager gpgsManager;
    private Text textComponent;

    private void Start()
    {
        textComponent = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = gpgsManager.SignedIn ? "Signed In" : "Not signed in";
    }
}
