using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("WorldMapScene"); //GameScene
    }


    public void Options()
    {
        SceneManager.LoadScene("OptiosnScene"); //OptionsScene
    }

    public void Quit()
    {
        Debug.Log("Quit button clicked");
    }
}
