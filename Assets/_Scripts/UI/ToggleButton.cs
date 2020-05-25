using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleButton : MonoBehaviour
{
    public Image buttonIcon;
    public Sprite onSprite, offSprite;
    public SBool isOn;
    public UnityEvent OnToggled;

    private Button buttonComponent;

    void Start()
    {
        buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.AddListener(Toggle);
        buttonIcon.sprite = isOn.Value ? onSprite : offSprite;
    }

    private void Toggle()
    {
        isOn.Value = !isOn.Value;
        if (OnToggled != null)
            OnToggled.Invoke();

        buttonIcon.sprite = isOn.Value ? onSprite : offSprite;
    }
}