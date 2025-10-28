using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;          // Prefab del enemigo que se instanciará.
    public int rows = 4;                    // Cantidad de filas de enemigos.
    public int cols = 8;                    // Cantidad de columnas de enemigos.
    public Vector2 spacing = new Vector2(1.5f, 1.0f); // Distancia entre enemigos en X y Y.

    public float speed = 1.5f;              // Velocidad de desplazamiento horizontal del grupo.
    public float descendAmount = 0.5f;      // Distancia que baja el grupo al cambiar de dirección.

    public Transform rightBound;            // Límite derecho del área de movimiento.
    public Transform leftBound;             // Límite izquierdo del área de movimiento.

    Transform container;                    // Contenedor principal que agrupa a todos los enemigos.
    float direction = 1f;                   // Dirección actual del movimiento (1 = derecha, -1 = izquierda).
    float startX;                           // Posición inicial en X del contenedor.

    List<GameObject> enemies = new List<GameObject>(); // Lista para rastrear los enemigos activos.

    void Start()
    {
        container = new GameObject("EnemyContainer").transform; // Crea un objeto vacío para agrupar enemigos.
        SpawnGrid();                                            // Genera la cuadrícula de enemigos.
        startX = container.position.x;                          // Guarda la posición inicial para cálculos posteriores.
    }

    void SpawnGrid()
    {
        // Calcula el punto inicial en base al tamaño de la cámara principal.
        Camera cam = Camera.main;
        Vector2 topLeft = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, 0)); // Esquina superior izquierda de la cámara.
        Vector2 start = topLeft + new Vector2(spacing.x / 2, -spacing.y / 2);         // Ajuste del punto de inicio para centrar la grilla.

        // Crea una matriz de enemigos según filas y columnas definidas.
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Vector2 pos = start + new Vector2(c * spacing.x, -r * spacing.y);      // Calcula la posición individual del enemigo.
                GameObject e = Instantiate(enemyPrefab, pos, Quaternion.identity, container); // Instancia el enemigo como hijo del contenedor.
                enemies.Add(e);                                                       // Lo añade a la lista de enemigos activos.
            }
        }

        container.position = Vector3.zero; // Centra la posición del contenedor en el origen.
    }

    void Update()
    {
        // Movimiento horizontal del grupo de enemigos.
        float moveStep = speed * Time.deltaTime * direction;
        container.position += Vector3.right * moveStep;

        // Cálculo de los bordes reales del grupo en base a su posición actual.
        float gridLeft = container.position.x + startX;
        float gridRight = gridLeft + (cols - 1) * spacing.x;

        // Cambia la dirección del movimiento al llegar a los límites y desciende ligeramente.
        if (gridRight >= rightBound.position.x && direction > 0)
        {
            direction = -1f;
            container.position += Vector3.down * descendAmount;
        }
        else if (gridLeft <= leftBound.position.x && direction < 0)
        {
            direction = 1f;
            container.position += Vector3.down * descendAmount;
        }

        // Limpia la lista eliminando referencias nulas (enemigos destruidos).
        enemies.RemoveAll(e => e == null);

        // Si todos los enemigos fueron destruidos, activa el estado de victoria.
        if (enemies.Count == 0)
        {
            FindObjectOfType<GameManager>()?.Win();
        }
    }
}