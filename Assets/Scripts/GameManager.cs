using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Player Player1;
    public Player Player2;

    private void Awake()
    {
        instance = this;
    }

    public void CheckForEnd()
    {
        if (Player1.Arrived && Player2.Arrived)
        {
            Debug.Log("reset");
            Player1.Reset();
            Player2.Reset();
        }
    }
}
