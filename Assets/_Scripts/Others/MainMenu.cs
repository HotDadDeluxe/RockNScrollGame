using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //[SerializeField] private GameObject mainMenuUI;
    //[SerializeField] private GameObject optionsMenuUI;

    public void Play()
    {
        // SceneManager.LoadScene("WorldMapScene"); //GameScene
        GameManager.Instance.SetState(GameManager.GameState.LevelSelection);
    }

    public void ShowControls()
    {
        GameManager.Instance.SetState(GameManager.GameState.Controls);
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.SetState(GameManager.GameState.MainMenu);
    }


    //public void Options()
    //{
    //    SceneManager.LoadScene("OptiosnScene"); //OptionsScene
    //}

    public void Quit()
    {
        Application.Quit();
    }
}
