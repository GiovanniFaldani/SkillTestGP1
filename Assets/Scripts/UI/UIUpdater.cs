using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    // Singleton managed by GameManager
    public static UIUpdater instance {  get; private set; }

    [SerializeField] GameObject menuScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject winScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        // Display main menu
        DisplayMainMenu();

    }

    public void DisplayMainMenu()
    {
        menuScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
        menuScreen.SetActive(false);
    }

    public void DisplayPauseMenu()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void UnPause()
    {
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);
    }

    public void DisplayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void DisplayWinScreen()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
