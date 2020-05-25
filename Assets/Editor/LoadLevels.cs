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
        EditorSceneManager.OpenScene("Assets/_Scenes/GameLevel.unity", OpenSceneMode.Additive);
    }
}
