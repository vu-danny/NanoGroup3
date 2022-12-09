using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerInputManager manager;
    [SerializeField] private HUD HUD;

    public Camera StartCamera;
    public List<Transform> StartingPoints;

    private Player Player1;
    private Player Player2;

    private void Awake()
    {
        manager = GetComponent<PlayerInputManager>();
        instance = this;
    }

    public void StartGame()
    {
        Debug.Log("start");
        Player1.StartRun();
        Player2.StartRun();
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

    public void OnPlayerJoined(PlayerInput input)
    {
        if (input.playerIndex == 0)
        {
            Player1 = input.GetComponentInChildren<Player>();
        }
        else 
        {
            Player2 = input.GetComponentInChildren<Player>();
        }

        input.gameObject.transform.position = StartingPoints[input.playerIndex].position;
        
        StartCamera.gameObject.SetActive(false);
        if (manager.playerCount == manager.maxPlayerCount)
            HUD.Countdown.ToggleAnimator();
    }
}
