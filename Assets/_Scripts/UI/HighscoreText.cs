using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreText : MonoBehaviour
{
    public ScoreManager scoreManager;
    public SFloat distanceTravelled;
    public HighscoreManager highscoreManager;
    private Text textComponent;
    private string highscoreText;

    void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    private void OnEnable()
    {
        SetText();
    }


    private void SetText()
    {
        Highscore bestHighscore = highscoreManager.GetHighestScore();
        textComponent.text = string.Format("Score: {0}\nBest: {1}", scoreManager.score.Value, bestHighscore != null ? bestHighscore._score : scoreManager.score.Value);
    }
}
