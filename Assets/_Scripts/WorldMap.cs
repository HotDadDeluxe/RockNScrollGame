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
}
