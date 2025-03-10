using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject levelCompleteUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject pauseMenuUI;
    public static bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        GameManager.Instance.SetState(GameManager.GameState.Paused);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void endLevel()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SetState(GameManager.GameState.LevelComplete);
        SceneManager.LoadScene("LevelOneEndScene");
    }

    public void gotoMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SetState(GameManager.GameState.MainMenu);
    }

    public void quitGame()
    {
        Application.Quit();
    }
    public void ShowLevelCompleteUI()
    {
        levelCompleteUI.SetActive(true);
    }
}