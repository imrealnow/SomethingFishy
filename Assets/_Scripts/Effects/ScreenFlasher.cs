using UnityEngine;
using UnityEngine.UI;

public class ScreenFlasher : MonoBehaviour
{

    private Image panelImage;


    void Start()
    {
        panelImage = GetComponent<Image>();
        panelImage.enabled = false;
    }

    public void FlashScreen(float duration)
    {
        panelImage.enabled = true;
        Invoke("UnflashScreen", duration);
    }

    private void UnflashScreen()
    {
        panelImage.enabled = false;
    }
}
