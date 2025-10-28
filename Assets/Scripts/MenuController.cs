using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;   // Panel del menú principal.
    public GameObject hudCanvas;       // Canvas que contiene la UI del juego (score, vidas, etc.).
    public GameObject gameOverPanel;   // Panel de Game Over (opcional, se puede activar desde GameManager).
    public GameObject winPanel;        // Panel de Victoria (opcional, se puede activar desde GameManager).

    void Start()
    {
        // Si el menú principal está activo al iniciar, pausa el juego y oculta la UI del HUD.
        if (mainMenuPanel != null && mainMenuPanel.activeSelf)
        {
            Time.timeScale = 0f;             // Pausa el tiempo del juego.
            if (hudCanvas) hudCanvas.SetActive(false); // Oculta el HUD mientras el menú está activo.
        }
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);     // Oculta el menú principal.
        hudCanvas.SetActive(true);          // Activa la UI del juego.
        Time.timeScale = 1f;                // Reanuda el tiempo del juego.
    }

    public void GoToMainMenu()
    {
        // Recarga la escena actual para reiniciar el juego.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit(); // Cierra la aplicación compilada.
    }
}