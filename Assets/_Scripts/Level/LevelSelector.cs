using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void LoadIntro(int levelIndex)
    {
        if (levelIndex > GameManager.Instance.MaxLevelUnlocked)
        {
            Debug.LogError("Invalid level index: " + levelIndex);
            return;
        }
        else if (levelIndex == -1)
        {
            Debug.Log("GameManager.GameState.MainMenu");
            GameManager.Instance.SetState(GameManager.GameState.MainMenu);
            
            return;
        }
        Debug.Log("GameManager set current level " + levelIndex);
        GameManager.Instance.SetCurrentLevel(levelIndex);
        GameManager.Instance.SetState(GameManager.GameState.Intro);
        Debug.Log("loading level " + levelIndex);
    }
}
