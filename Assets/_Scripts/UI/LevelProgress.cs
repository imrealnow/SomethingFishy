using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public RectTransform start, end, currentProgress;
    public GameManager gameManager;

    private float scale;

    void Update()
    {
        scale = end.position.x - start.position.x;
        currentProgress.position = new Vector3(
            start.position.x + gameManager.LevelProgress * scale,
            currentProgress.position.y,
            currentProgress.position.z
            );
    }
}
