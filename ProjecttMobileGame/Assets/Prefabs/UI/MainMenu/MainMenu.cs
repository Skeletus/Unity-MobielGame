using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button StartBtn;
    [SerializeField] Button ControlsBtn;
    [SerializeField] Button BackBtn;
    [SerializeField] CanvasGroup FrontUI;
    [SerializeField] CanvasGroup ControllsUI;
    [SerializeField] LevelManager levelManager;
    private void Start()
    {
        StartBtn.onClick.AddListener(StartGame);
        ControlsBtn.onClick.AddListener(SwithToControlUI);
        BackBtn.onClick.AddListener(SwitchToFrontUI);
    }

    private void SwitchToFrontUI()
    {
        ControllsUI.blocksRaycasts = false;
        ControllsUI.alpha = 0;

        FrontUI.blocksRaycasts = true;
        FrontUI.alpha = 1;
    }

    private void SwithToControlUI()
    {
        ControllsUI.blocksRaycasts = true;
        ControllsUI.alpha = 1;

        FrontUI.blocksRaycasts = false;
        FrontUI.alpha = 0;
    }

    private void StartGame()
    {
        levelManager.LoadFirstLevel();
    }
}
