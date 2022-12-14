using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickUI : MonoBehaviour
{
    private Image Joystick;
    private Vector2 InitJoystickPos;

    private void Awake()
    {
        Joystick = GetComponent<Image>();
        InitJoystickPos = Joystick.rectTransform.anchoredPosition;
    }

    public void MoveJoystick(Vector2 gap)
    {
        Joystick.rectTransform.anchoredPosition = InitJoystickPos + new Vector2(Mathf.Clamp(gap.x, -10f, 10f), 
            Mathf.Clamp(gap.y, -10f, 10f));
    }
}
