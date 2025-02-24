using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("CutScene"); //Loads The cutscene
        Debug.Log("Play button clicked");
    }


    public void Options()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); //OptionsScene
        Debug.Log("Options button clicked");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit button clicked");
    }
}
