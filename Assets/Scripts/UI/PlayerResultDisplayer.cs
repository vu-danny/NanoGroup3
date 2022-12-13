using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResultDisplayer : MonoBehaviour
{
    [SerializeField] Sprite bigBallIcon;
    [SerializeField] Sprite speedIcon;
    [SerializeField] Sprite blankIcon;

    [SerializeField] Image ballImage;
    [SerializeField] Image speedImage;
    [SerializeField] TMP_Text playerText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text sizeText;

    public float ballSize;
    
    public void InitializeValues(bool first, int playerNumber, float timeSeconds, float size){
        speedImage.sprite = first ? speedIcon : blankIcon;
        playerText.text = "Joueur "+playerNumber;
        int seconds = Mathf.FloorToInt(timeSeconds);
        int ms = Mathf.FloorToInt((timeSeconds - seconds)*100);
        int minutes = seconds/60;
        seconds %= 60;
        timeText.text = minutes.ToString("D2")+":"+seconds.ToString("D2")+":"+ms.ToString("D2");
        sizeText.text = string.Format("{0:0.00}", size) +" m";
        ballSize = size;
    }

    public void UpdateBall(bool biggest){
        ballImage.sprite = biggest?bigBallIcon:blankIcon;
    }
}
