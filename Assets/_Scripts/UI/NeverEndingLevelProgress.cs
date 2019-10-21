using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeverEndingLevelProgress : MonoBehaviour
{
    public RectTransform start, end, currentProgress;
    public GameManager gameManager;

    private float scale;

    void Update()
    {
        scale = end.position.x - start.position.x;
        float levelProgress = gameManager.LevelProgress * (1 - gameManager.LevelProgress);
        currentProgress.position = new Vector3(
            start.position.x + levelProgress * scale,
            currentProgress.position.y,
            currentProgress.position.z
            );
    }
}
