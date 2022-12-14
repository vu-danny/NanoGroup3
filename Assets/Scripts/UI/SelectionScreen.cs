using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    [SerializeField] private Button Start;
    [SerializeField] private Image Controller1;
    [SerializeField] private Image Controller2;
    
    [SerializeField] private Sprite SelectedController;
    [SerializeField] private Sprite UnselectedController;

    private void Awake()
    {
        Controller1.sprite = UnselectedController;
        Controller2.sprite = UnselectedController;
        Start.interactable = false;
    }

    public void SetController(int number, bool on)
    {
        if (number == 1)
        {
            Controller1.sprite = on ? SelectedController : UnselectedController;
        } 
        else if (number == 2)
        {
            Controller2.sprite = on ? SelectedController : UnselectedController;
        }
        Start.interactable = Controller1.sprite == SelectedController && Controller2.sprite == SelectedController;
    }
}
