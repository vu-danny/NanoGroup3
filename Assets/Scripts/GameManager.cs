using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerInputManager manager;
    [SerializeField] private HUD HUD;
    public SelectionScreen SelectionScreen;

    public Camera StartCamera;
    public List<Transform> StartingPoints;

    [System.NonSerialized] public Player Player1;
    [SerializeField] private LayerMask Player1LayerMask;
    [System.NonSerialized] public Player Player2;
    [SerializeField] private LayerMask Player2LayerMask;
    [SerializeField] private Snowman Snowman1;
    [SerializeField] private Snowman Snowman2;

    [SerializeField] private GameObject GameFinishedUI;
    [SerializeField] private GameObject progressTracker;

    [System.NonSerialized] public FMOD.Studio.EventInstance Music;
    private void Awake()
    {
        manager = GetComponent<PlayerInputManager>();
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
        instance = this;
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
            HUD.gameObject.SetActive(false);
            GameFinishedUI.SetActive(true);
            GameFinishedUI.GetComponent<EndUI>().Activate();
        }
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        if (input.playerIndex == 0)
        {
            Player1 = input.GetComponentInChildren<Player>();
            Player1.Snowman = Snowman1;
            Snowman1._player = Player1;
            Player1._camera.GetUniversalAdditionalCameraData().volumeLayerMask = Player1LayerMask;
            Player1._camera.GetComponent<CameraController>().SetVFXLayer(Player1LayerMask);
            
            SelectionScreen.SetController(1, true);
        }
        else 
        {
            Player2 = input.GetComponentInChildren<Player>();
            Player2.Snowman = Snowman2;
            Snowman2._player = Player2;
            Player2._camera.GetUniversalAdditionalCameraData().volumeLayerMask = Player2LayerMask;
            Player2._camera.GetComponent<CameraController>().SetVFXLayer(Player2LayerMask);
            
            SelectionScreen.SetController(2, true);
        }

        input.gameObject.transform.position = StartingPoints[input.playerIndex].position;


        if (manager.playerCount == 1)
            StartCamera.rect = new Rect(0.5f, 0f, 1f, 1f);
            else if(manager.playerCount == 2)
            StartCamera.gameObject.SetActive(false);
        if (manager.playerCount >= manager.maxPlayerCount)
            manager.DisableJoining();
    }

    public void StartCountDown()
    {
        SelectionScreen.gameObject.SetActive(false);
        HUD.Countdown.ToggleAnimator();
    }
}
