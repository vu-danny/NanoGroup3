using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private Animator Animator;
    public FMOD.Studio.EventInstance Music;
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void ToggleAnimator()
    {
        Animator.enabled = !Animator.enabled;
    }

    public void StartGameEvent()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UIBellStart");
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
        Music.start();
        GameManager.instance.StartGame();
    }

    public void CountSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UIBellCount");
    }
}
