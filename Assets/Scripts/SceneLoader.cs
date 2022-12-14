using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public void LoadTitle(){
        SceneManager.LoadScene("Title");
        if(GameManager.instance != null)
            GameManager.instance.StopMusic();
    }

    public void LoadMain(){
        SceneManager.LoadScene("Main");
        if(GameManager.instance != null)
            GameManager.instance.StopMusic();
    }

    public void LoadAScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit !");
    }
}
