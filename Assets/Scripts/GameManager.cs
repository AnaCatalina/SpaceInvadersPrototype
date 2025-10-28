using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;                      // Puntaje actual del jugador.
    public int lives = 3;                      // Cantidad de vidas disponibles.
    public TextMeshProUGUI scoreText;          // Referencia al texto UI que muestra el puntaje.
    public TextMeshProUGUI livesText;          // Referencia al texto UI que muestra las vidas.
    public GameObject gameOverPanel;           // Panel UI que se muestra al perder todas las vidas.
    public GameObject winPanel;                // Panel UI que se muestra al ganar la partida.

    void Start()
    {
        ResetUI();                             // Inicializa los valores y oculta los paneles al comenzar el juego.
    }

    void ResetUI()
    {
        score = 0;                             // Reinicia el puntaje.
        lives = 3;                             // Restaura las vidas iniciales.
        UpdateUI();                            // Actualiza los textos en pantalla.
        if (gameOverPanel) gameOverPanel.SetActive(false); // Oculta el panel de Game Over si existe.
        if (winPanel) winPanel.SetActive(false);           // Oculta el panel de Victoria si existe.
    }

    public void AddScore(int v)
    {
        score += v;                            // Incrementa el puntaje con el valor recibido.
        UpdateUI();                            // Actualiza el texto en la UI.
    }

    public void LoseLife()
    {
        lives = Mathf.Max(lives - 1, 0);       // Resta una vida sin permitir valores negativos.
        UpdateUI();                            // Actualiza la UI con las vidas restantes.
        if (lives <= 0) GameOver();            // Si no quedan vidas, activa el Game Over.
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score; // Actualiza el texto del puntaje.
        if (livesText) livesText.text = "Lives: " + lives; // Actualiza el texto de vidas.
    }

    void GameOver()
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);  // Muestra el panel de Game Over.
        Time.timeScale = 0f;                               // Pausa el juego deteniendo el tiempo.
    }

    public void Win()
    {
        if (winPanel) winPanel.SetActive(true);            // Muestra el panel de Victoria.
        Time.timeScale = 0f;                               // Pausa el juego al ganar.
    }
}