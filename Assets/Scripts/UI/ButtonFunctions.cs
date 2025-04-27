using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void StartGame()
    {
        UIUpdater.instance.StartGame();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void UnPauseGame()
    {
        UIUpdater.instance.UnPause();
        GameManager.instance._paused = false;
    }
}
