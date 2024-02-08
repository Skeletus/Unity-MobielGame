using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] Button ResumeBtn;
    [SerializeField] Button RestartBtn;
    [SerializeField] Button MainMenu;
    [SerializeField] UIManager uiManager;
    [SerializeField] LevelManager levelManager;

    private void Start()
    {
        ResumeBtn.onClick.AddListener(ResumeGame);
        RestartBtn.onClick.AddListener(RestartLevel);
        MainMenu.onClick.AddListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        //Debug.Log("Back to Main");
        levelManager.GoToMainMenu();
    }

    private void RestartLevel()
    {
        //Debug.Log("Restart Level");
        levelManager.RestartCurrentLevel();
    }

    private void ResumeGame()
    {
        uiManager.SwithToGameplayUI();
    }
}
