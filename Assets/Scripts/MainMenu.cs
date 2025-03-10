using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject optionsMenuUI;

    // void Awake()
    // {
    //     GameManager.OnGameStateChanged += GamaeManager_OnGameStateChanged;
    // }

    // void OnDestrory()
    // {
    //     GameManager.OnGameStateChanged -= GamaeManager_OnGameStateChanged;
    // }

    // private void GamaeManager_OnGameStateChanged(GameState state)
    // {

    //     switch (state)
    //     {
    //         case GameState.MainMenu:
    //             mainMenuUI.SetActive(true);
    //             optionsMenuUI.SetActive(false);
    //             break;
    //         case GameState.LevelSelection:
    //             mainMenuUI.SetActive(false);
    //             optionsMenuUI.SetActive(false);
    //             break;
    //         case GameState.Playing:
    //             mainMenuUI.SetActive(false);
    //             optionsMenuUI.SetActive(false);
    //             break;
    //         default:
    //             break;
    //     }
    // }
    public void Play()
    {
        // SceneManager.LoadScene("WorldMapScene"); //GameScene
        GameManager.Instance.SetState(GameManager.GameState.LevelSelection);
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
