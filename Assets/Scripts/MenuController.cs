using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject hudCanvas;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    void Start()
    {
        if (mainMenuPanel != null && mainMenuPanel.activeSelf)
        {
            Time.timeScale = 0f;
            if (hudCanvas) hudCanvas.SetActive(false);
        }
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        hudCanvas.SetActive(true);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}