using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    [SerializeField] private Button Start;
    [SerializeField] private Image Controller1;
    [SerializeField] private Image Controller2;
    [SerializeField] private TextMeshProUGUI Player1Name;
    [SerializeField] private TextMeshProUGUI Player2Name;
    [SerializeField] private JoystickUI JoystickPlayer1;
    [SerializeField] private JoystickUI JoystickPlayer2;
    
    [SerializeField] private Sprite SelectedController;
    [SerializeField] private Sprite UnselectedController;
    

    private void Awake()
    {
        SetController(1, false);
        SetController(2, false);
        Start.interactable = false;
    }

    public void SetController(int number, bool on)
    {
        if (number == 1)
        {
            Controller1.sprite = on ? SelectedController : UnselectedController;
            Player1Name.text = on ? "Joueur 1" : " ";
            if(on)
                GameManager.instance.Player1.JoystickUI = JoystickPlayer1;
        } 
        else if (number == 2)
        {
            Controller2.sprite = on ? SelectedController : UnselectedController;
            Player2Name.text = on ? "Joueur 2" : " ";
            if(on)
                GameManager.instance.Player2.JoystickUI = JoystickPlayer2;
        }
        Start.interactable = Controller1.sprite == SelectedController && Controller2.sprite == SelectedController;
    }
}
