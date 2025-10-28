using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int scoreValue = 100;

    public void Die()
    {
        FindObjectOfType<GameManager>()?.AddScore(scoreValue);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Poner vidas a 0
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.lives = 0;
                gm.LoseLife(); // esto actualiza UI y activa GameOver
            }

            // Desaparecer el jugador
            Destroy(other.gameObject);
        }
    }
}