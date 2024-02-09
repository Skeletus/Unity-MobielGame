using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Auido/UIAudioPlayer")]
public class UIAudioPlayer : ScriptableObject
{
    [SerializeField] AudioClip ClickAudioClip;
    [SerializeField] AudioClip CommitAudioClip;
    [SerializeField] AudioClip SelectAudioClip;

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

    void PlayAudio(AudioClip audioToPlay)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(audioToPlay);
    }
}
