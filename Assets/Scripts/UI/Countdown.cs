using System;
using System.Collections;
using System.Collections.Generic;
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
        GameManager.instance.StartGame();
    }
}
