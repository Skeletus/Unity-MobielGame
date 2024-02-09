using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Auido/UIAudioPlayer")]
public class UIAudioPlayer : ScriptableObject
{
    [SerializeField] AudioClip ClickAudioClip;
    [SerializeField] AudioClip CommitAudioClip;
    [SerializeField] AudioClip SelectAudioClip;
    [SerializeField] AudioClip WinAudioClip;

    public void PlayClick()
    {
        PlayAudio(ClickAudioClip);
    }

    public void PlayCommit()
    {
        PlayAudio(CommitAudioClip);
    }

    public void PlaySelect()
    {
        PlayAudio(SelectAudioClip);
    }

    internal void PlayWin()
    {
        PlayAudio(WinAudioClip);
    }

    void PlayAudio(AudioClip audioToPlay)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(audioToPlay);
    }
}
