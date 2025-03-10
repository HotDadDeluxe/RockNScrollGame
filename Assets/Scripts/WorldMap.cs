using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WorldMap : MonoBehaviour
{
    public void loadCutscene(int levelIndex)
    {
        if (levelIndex > GameManager.Instance.MaxLevelUnlocked)
        {
            Debug.LogError("Invalid level index: " + levelIndex);
            return;
        }
        else if (levelIndex == -1)
        {
            GameManager.Instance.SetState(GameManager.GameState.MainMenu);
            Debug.Log("loading main menu");
            return;
        }

        GameManager.Instance.SetCurrentLevel(levelIndex);
        GameManager.Instance.SetState(GameManager.GameState.CutsceneStart);
        Debug.Log("loading level " + levelIndex);
    }

    // public void StartLevel1()
    // {
    //     GameManager.Instance.SetCurrentLevel(1);
    //     GameManager.Instance.SetState(GameManager.GameState.CutsceneStart);
    //     // SceneManager.LoadScene("CutScene1"); //Level1Scene
    //     Debug.Log("loading level 1");
    // }

    // public void StartLevel2()
    // {
    //     GameManager.Instance.SetCurrentLevel(2);
    //     GameManager.Instance.SetState(GameManager.GameState.CutsceneStart);
    //     SceneManager.LoadScene("CutScene2"); //Level2Scene
    //     Debug.Log("loading level 2");
    // }

    // public void StartLevel3()
    // {
    //     SceneManager.LoadScene("Level 3"); //Level3Scene
    //     Debug.Log("loading level 3");
    // }

    // public void StartLevel4()
    // {
    //     SceneManager.LoadScene("Level 4"); //Level4Scene
    //     Debug.Log("loading level 4");
    // }

    // public void StartTest()
    // {
    //     GameManager.Instance.SetCurrentLevel(0);
    //     GameManager.Instance.SetState(GameManager.GameState.CutsceneStart);
    //     Debug.Log("loading test level");
    // }
}
