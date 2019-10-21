using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;

public class LoadLevels
{
    [MenuItem("Load Level/Main Menu")]
    private static void LoadMainMenu()
    {
        EditorSceneManager.OpenScene("Assets/_Scenes/MainMenu.unity", OpenSceneMode.Single);
    }

    [MenuItem("Load Level/First Level")]
    private static void LoadFirstLevel()
    {
        EditorSceneManager.OpenScene("Assets/_Scenes/PersistentObjects.unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/_Scenes/GameLevels/FirstLevel.unity", OpenSceneMode.Additive);
    }

    [MenuItem("Load Level/Second Level")]
    private static void LoadSecondLevel()
    {
        EditorSceneManager.OpenScene("Assets/_Scenes/PersistentObjects.unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/_Scenes/GameLevels/SecondLevel.unity", OpenSceneMode.Additive);
    }

    [MenuItem("Load Level/Third Level")]
    private static void LoadThirdLevel()
    {
        EditorSceneManager.OpenScene("Assets/_Scenes/PersistentObjects.unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/_Scenes/GameLevels/ThirdLevel.unity", OpenSceneMode.Additive);
    }
}
