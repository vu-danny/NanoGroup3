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

    [System.NonSerialized] public Player Player1;
    [System.NonSerialized] public Player Player2;
    [SerializeField] private Snowman Snowman1;
    [SerializeField] private Snowman Snowman2;

    [SerializeField] private GameObject GameFinishedUI;
    [SerializeField] private GameObject progressTracker;

    private void Awake()
    {
        manager = GetComponent<PlayerInputManager>();
        instance = this;
        GameFinishedUI.SetActive(false);
    }

    public void StartGame()
    {
        Player1.number = 1;
        Player2.number = 2;
        Player1.StartRun();
        Player2.StartRun();
        progressTracker.GetComponent<ProgressTracker>().AddPlayerTransform(Player1.transform);
        progressTracker.GetComponent<ProgressTracker>().AddPlayerTransform(Player2.transform);
        progressTracker.SetActive(true);
    }

    public void CheckForEnd()
    {
        if (Player1.Arrived && Player2.Arrived)
        {
            GameFinishedUI.SetActive(true);
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
        if (manager.playerCount >= manager.maxPlayerCount)
        {
            manager.DisableJoining();
            HUD.Countdown.ToggleAnimator();
        }
    }
}
