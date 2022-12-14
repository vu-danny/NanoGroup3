using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private Animator Animator;
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
        GameManager.instance.Music.start();
        GameManager.instance.StartGame();
    }

    public void CountSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UIBellCount");
    }
}
