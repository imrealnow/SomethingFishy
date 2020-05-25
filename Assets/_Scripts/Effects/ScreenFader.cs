using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ScreenFader : MonoBehaviour
{
    public AnimationCurve fadeCurve;
    public float duration;
    public UnityEvent OnFadeComplete;
    [Space]
    public bool fadeOnStart;
    public bool fadeOnEnable;

    private Image screenPanel;
    private Color startColor;

    void Start()
    {
        startColor = screenPanel.color;

        if (fadeOnStart)
            StartCoroutine(Fader(duration));
    }

    void OnEnable()
    {
        screenPanel = GetComponent<Image>();
        if (fadeOnEnable)
            StartCoroutine(Fader(duration));
    }

    public void FadeScreen(float duration)
    {
        StartCoroutine(Fader(duration));
    }

    private IEnumerator Fader(float duration)
    {
        float endTime = Time.realtimeSinceStartup + duration;
        while (Time.realtimeSinceStartup < endTime)
        {
            Color newColor = startColor;
            float fadePercent = (endTime - Time.realtimeSinceStartup) / duration;
            newColor.a = fadeCurve.Evaluate(fadePercent);
            screenPanel.color = newColor;
            yield return null;
        }
        if (OnFadeComplete != null)
            OnFadeComplete.Invoke();
    }
}
