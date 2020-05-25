using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(fileName = "GPGSManager", menuName = "SO/Managers/GPGSManager", order = 1)]
public class GPGSManager : SManager
{

    public const string leaderboardID = "CgkIoKi5n80LEAIQAQ";

    [SerializeField]
    private bool initialised = false;
    [SerializeField]
    private bool signedIn = false;

    public bool SignedIn
    {
        get { return signedIn; }
    }

    public override void OnEnabled()
    {
        if (Time.realtimeSinceStartup < 6)
            initialised = false;
        if (!initialised)
            InitialisePlayServices();
    }


    private void InitialisePlayServices()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        initialised = true;
        SignIn();
    }

    public void SignIn()
    {
        if (!Social.Active.localUser.authenticated)
        {
            Social.Active.localUser.Authenticate((bool success) =>
            {
                signedIn = success;
                Debug.Log(success ? "GPGS successfully signed in" : "GPGS sign-in failed");
            });
        }
    }

    public void ShowLeaderboard()
    {
        if (!signedIn)
            return;

        PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardID);
    }

    public void SaveHighscore(int score)
    {
        if (!signedIn)
            return;

        Social.ReportScore(score, leaderboardID, (bool success) =>
        {
            // handle success or failure
        });
    }

}
