using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnNewHighscoreSet : MonoBehaviour
{
    public ScoreManager scoreManager;
    public HighscoreManager highscoreManager;

    public UnityEvent onHighscoreSet;

    private void OnEnable()
    {
        CheckScore();
    }

    private void CheckScore()
    {
        if (scoreManager.score.Value == highscoreManager.GetHighestScore()._score)
            onHighscoreSet.Invoke();
    }
}
