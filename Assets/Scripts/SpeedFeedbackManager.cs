using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SpeedFeedbackManager : MonoBehaviour
{
    [Tooltip("This value is read only, and has been remaped from min-max speeds to 0-1"),SerializeField]
    private float player1Speed, player2Speed;
    [SerializeField]
    private Volume player1Volume, player2Volume;

    [SerializeField]
    private List<GameObject> sameLayerAsPlayer;
    [SerializeField]
    private List<GameObject> oppositeLayerAsPlayer;
    private int playerLayer, opponentLayer;
    public LayerMask p1;

    // Update is called once per frame
    void Update()
    {
        player1Volume.weight = 1-player1Speed;
        player2Volume.weight = 1-player2Speed;    

        Camera.main.GetUniversalAdditionalCameraData().volumeLayerMask = p1;
    }

    private void OnCamInstantiation(){
        foreach (var item in sameLayerAsPlayer)
        {
            item.layer = playerLayer;
        }
        foreach (var item in oppositeLayerAsPlayer)
        {
            item.layer = opponentLayer;
        }
    }
}
