using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int lives = 3;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    void Start()
    {
        ResetUI();
    }

    void ResetUI()
    {
        score = 0;
        lives = 3;
        UpdateUI();
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (winPanel) winPanel.SetActive(false);
    }

    public void AddScore(int v)
    {
        score += v;
        UpdateUI();
    }

    public void LoseLife()
    {
        lives = Mathf.Max(lives - 1, 0); // nunca negativo
        UpdateUI();
        if (lives <= 0) GameOver();
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (livesText) livesText.text = "Lives: " + lives;
    }

    void GameOver()
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win()
    {
        if (winPanel) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}