using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WorldMap : MonoBehaviour
{
    public void StartLevel1()
    {
        SceneManager.LoadScene("CutScene1"); //Level1Scene
        Debug.Log("loading level 1");
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("CutScene2"); //Level2Scene
        Debug.Log("loading level 2");
    }

    public void StartLevel3()
    {
        SceneManager.LoadScene("Level 3"); //Level3Scene
        Debug.Log("loading level 3");
    }

    public void StartLevel4()
    {
        SceneManager.LoadScene("Level 4"); //Level4Scene
        Debug.Log("loading level 4");
    }

    public void StartTest()
    {
        SceneManager.LoadScene("GameScene"); //test
        Debug.Log("loading test level");
    }
}
