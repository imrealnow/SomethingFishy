using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour {
    public List<Color> flashColors = new List<Color>();
    public float duration;
    [Range(0f,1f)]
    public float maxIntensity;
    public AnimationCurve intensityCurve;

    public Color tintColor
    {
        get { return _renderer.material.GetColor("_TintColor"); }
        set
        {
            _renderer.material.SetColor("_TintColor", value);
        }
    }

    private Renderer _renderer;

	void Awake ()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnDisable()
    {
        _renderer.material.SetFloat("_Intensity", 0);
        StopAllCoroutines();
    }

    public void Flash(int colorIndex)
    {
        Color colorToFlash;
        if (flashColors.Count == 0)
            colorToFlash = Color.white;
        else
            colorToFlash = flashColors[colorIndex];

        StopAllCoroutines();
        StartCoroutine(Flasher(colorToFlash));
    }

	IEnumerator Flasher(Color colorToFlash)
    {
        _renderer.material.SetColor("_FlashColor", colorToFlash);

        float endTime = Time.time + duration;
        while(Time.time < endTime)
        {
            _renderer.material.SetFloat("_Intensity", intensityCurve.Evaluate((endTime - Time.time) / duration) * maxIntensity);
            yield return null;
        }

        _renderer.material.SetFloat("_Intensity", 0);
    }
}
