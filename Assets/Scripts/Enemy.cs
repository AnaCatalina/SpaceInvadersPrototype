using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int scoreValue = 50; // Puntaje que otorga este enemigo al ser destruido.

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
    private void OnDestroy()
    {
        FindFirstObjectByType<GameManager>()?.AddScore(scoreValue);
    }
}