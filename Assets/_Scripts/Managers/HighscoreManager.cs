using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HighscoreManager", menuName = "SO/Managers/HighscoreManager", order = 1)]
public class HighscoreManager : SManager
{
    private const string highscoreSaveTag = "highscores";
    public GPGSManager gpgsManager;
    public List<Highscore> highscores = new List<Highscore>();

    public Action newHighscoreSet;

    public override void OnEnabled()
    {
        LoadHighscores();
    }

    public override void OnDisabled()
    {
        SaveHighscores();
    }

    private void LoadHighscores()
    {
        highscores = PPSerialisation.Load(highscoreSaveTag) as List<Highscore>;
        if (highscores == null)
            highscores = new List<Highscore>();
    }

    private void SaveHighscores()
    {
        PPSerialisation.Save(highscoreSaveTag, highscores);
    }

    public void SaveHighscore(float distanceTravelled, int score)
    {
        Highscore newHighscore = new Highscore(distanceTravelled, score);
        highscores.Add(newHighscore);

        // Invoke the new highscore set event if the new highscore is the highest score
        if (GetHighestScore() == newHighscore && newHighscoreSet != null)
            newHighscoreSet.Invoke();

        gpgsManager.SaveHighscore(score);
        PPSerialisation.Save(highscoreSaveTag, highscores);
    }

    public Highscore GetHighestScore()
    {
        if (highscores.Count == 0)
            return null;

        Highscore highestScore = highscores[0];
        for(int i = 1; i < highscores.Count; i++)
        {
            if (highscores[i]._score > highestScore._score)
                highestScore = highscores[i];
        }

        return highestScore;
    }

    [ContextMenu("Reset Highscores")]
    private void ResetHighscores()
    {
        highscores = new List<Highscore>();
        SaveHighscores();
    }
}

[Serializable]
public class Highscore
{
    public float _distanceTravelled;
    public int _score;

    public Highscore(float distanceTravelled, int score)
    {
        _distanceTravelled = distanceTravelled;
        _score = score;
    }
}