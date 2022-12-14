using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFXManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance Click;
    private FMOD.Studio.EventInstance Selected;

    private void Awake()
    {
        Click = FMODUnity.RuntimeManager.CreateInstance("event:/UIConfirm");
        Selected = FMODUnity.RuntimeManager.CreateInstance("event:/UISelect");
    }

    public void ClickSFX()
    {
        Click.start();
    }
}
