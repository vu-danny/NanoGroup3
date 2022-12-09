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
    [SerializeField] private Snowman Snowman1;
    [SerializeField] private Snowman Snowman2;

    private void Awake()
    {
        manager = GetComponent<PlayerInputManager>();
        instance = this;
    }

    public void StartGame()
    {
        Player1.StartRun();
        Player2.StartRun();
    }

    public void CheckForEnd()
    {
        if (Player1.Arrived && Player2.Arrived)
        {
            Player1.Reset();
            Player2.Reset();
        }
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        if (input.playerIndex == 0)
        {
            Player1 = input.GetComponentInChildren<Player>();
            Player1.Snowman = Snowman1;
        }
        else 
        {
            Player2 = input.GetComponentInChildren<Player>();
            Player2.Snowman = Snowman2;
        }

        input.gameObject.transform.position = StartingPoints[input.playerIndex].position;
        
        StartCamera.gameObject.SetActive(false);
        if (manager.playerCount == manager.maxPlayerCount)
            HUD.Countdown.ToggleAnimator();
    }
}
