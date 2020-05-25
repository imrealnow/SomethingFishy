using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreSaver : MonoBehaviour
{
    public SFloat distanceTravelled;
    public SInt score;

    public HighscoreManager highscoreManager;

    public void SaveScore()
    {
        highscoreManager.SaveHighscore(distanceTravelled.Value, score.Value);
    }
}
