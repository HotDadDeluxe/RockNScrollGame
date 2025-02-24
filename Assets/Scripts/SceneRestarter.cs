using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    private static void RestartScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(currentScene);
    }
}
