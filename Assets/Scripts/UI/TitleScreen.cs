using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Button Play;
    [SerializeField] private Button Back;
    [SerializeField] private GameObject TitleScreenObject;
    [SerializeField] private GameObject InstructionScreenObject;
    
    public void LoadInstruction()
    {
        Back.Select();
        TitleScreenObject.SetActive(false);
        InstructionScreenObject.SetActive(true);
    }
    public void LoadTitleScreen()
    {
        Play.Select();
        TitleScreenObject.SetActive(true);
        InstructionScreenObject.SetActive(false);
    }
}
