using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomStringSetter : MonoBehaviour
{
    public Text textComponent;
    public List<string> randomStrings = new List<string>();
    public bool setOnEnable;

    void OnEnable()
    {
        if (setOnEnable)
            RandomiseString();
    }

    public void RandomiseString()
    {
        if (textComponent == null || randomStrings.Count == 0)
            return;

        int randomStringIndex = Random.Range(0, randomStrings.Count - 1);
        textComponent.text = randomStrings[randomStringIndex];
    }
}
