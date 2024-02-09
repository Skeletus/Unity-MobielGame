using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "LevelManager")]
public class LevelManager : ScriptableObject
{
    [SerializeField] int MainMenuBuildIndex = 0;
    [SerializeField] int FirstLevelBuildIndex = 1;

    public delegate void OnLevelFinished();
    public static event OnLevelFinished onLevelFinished;

    internal static void LevelFinished()
    {
        onLevelFinished?.Invoke();
    }

    public void GoToMainMenu()
    {
        LoadSceneByIndex(MainMenuBuildIndex);
    }

    public void LoadFirstLevel()
    {
        LoadSceneByIndex(FirstLevelBuildIndex);
    }

    public void RestartCurrentLevel()
    {
        LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
        GameplayStatics.SetGamePaused(false);
    }
}
