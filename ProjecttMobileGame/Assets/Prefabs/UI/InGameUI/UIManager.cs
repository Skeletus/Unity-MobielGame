using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup GameplayControl;
    [SerializeField] CanvasGroup PauseMenu;
    [SerializeField] CanvasGroup Shop;

    List<CanvasGroup> AllChildren = new List<CanvasGroup>();

    CanvasGroup currentActiveGrp;

    private void Start()
    {
        List<CanvasGroup> children = new List<CanvasGroup>();
        GetComponentsInChildren(true, children);
        foreach (CanvasGroup child in children)
        {
            if (child.transform.parent == transform)
            {
                AllChildren.Add(child);
                SetGroupActive(child, false, false);
            }
        }

        if (AllChildren.Count != 0)
        {
            SetCurrentActiveGrp(AllChildren[0]);
        }
    }

    internal void SwithToGameplayUI()
    {
        SetCurrentActiveGrp(GameplayControl);
        GameplayStatics.SetGamePaused(false);
    }

    private void SetCurrentActiveGrp(CanvasGroup canvasGroup)
    {
        if (currentActiveGrp != null)
        {
            SetGroupActive(currentActiveGrp, false, false);
        }

        currentActiveGrp = canvasGroup;
        SetGroupActive(currentActiveGrp, true, true);
    }

    private void SetGroupActive(CanvasGroup child, bool interactable, bool visible)
    {
        child.interactable = interactable;
        child.blocksRaycasts = interactable;
        child.alpha = visible ? 1 : 0;
    }

    public void SetGameplayControlEnabled(bool enabled)
    {
        SetCanvasGroupEnabled(GameplayControl, enabled);
    }

    public void SwithToPauseMenu()
    {
        SetCurrentActiveGrp(PauseMenu);
        GameplayStatics.SetGamePaused(true);
    }

    internal void SwithToShop()
    {
        SetCurrentActiveGrp(Shop);
        GameplayStatics.SetGamePaused(true);
    }

    private void SetCanvasGroupEnabled(CanvasGroup canvasGroup, bool enabled)
    {
        canvasGroup.interactable = enabled;
        canvasGroup.blocksRaycasts = enabled;
    }
}
