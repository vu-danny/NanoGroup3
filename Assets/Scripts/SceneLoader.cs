using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button Play;
    [SerializeField] private Button Back;
    [SerializeField] private GameObject TitleScreen;
    [SerializeField] private GameObject InstructionScreen;
    public void LoadTitle(){
        SceneManager.LoadScene("Title");
    }

    public void LoadMain(){
        SceneManager.LoadScene("Main");
    }

    public void LoadAScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit !");
    }

    public void LoadInstruction()
    {
        Back.Select();
        TitleScreen.SetActive(false);
        InstructionScreen.SetActive(true);
    }
    public void LoadTitleScreen()
    {
        Play.Select();
        TitleScreen.SetActive(true);
        InstructionScreen.SetActive(false);
    }
}
