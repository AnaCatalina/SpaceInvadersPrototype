using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int scoreValue = 100; // Puntaje que otorga este enemigo al ser destruido.
    public void Die()
    {
        // Suma puntaje al jugador al morir.
        FindFirstObjectByType<GameManager>()?.AddScore(scoreValue); // Busca el GameManager en la escena y llama a AddScore si existe.
        Destroy(gameObject);                                   // Destruye el enemigo actual.
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta colisiones con otros objetos marcados como "Trigger".
        if (other.CompareTag("Player")) // Si el objeto que colisiona tiene la etiqueta "Player"...
        {
            // Accede al GameManager para actualizar vidas y estado del juego.
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.lives = 0;      // Fuerza las vidas del jugador a 0.
                gm.LoseLife();     // Actualiza la interfaz y activa el Game Over.
            }

            // Elimina al jugador de la escena.
            Destroy(other.gameObject);
        }

        var band = EnemySpawner.Instance.band;
        switch (other.tag)
        {
            case "LeftBound":
                if (band)
                {
                    band = false;
                    EnemySpawner.ChangeHight();
                }
                break;
            case "RightBound":
                if (!band)
                {
                    band = true;
                    EnemySpawner.ChangeHight();
                }
                break;
        }
        EnemySpawner.Instance.band = band;
    }
}