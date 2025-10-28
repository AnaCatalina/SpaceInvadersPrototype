using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int rows = 4;
    public int cols = 8;
    public Vector2 spacing = new Vector2(1.5f, 1.0f);

    public float speed = 1.5f;
    public float descendAmount = 0.5f;

    public Transform rightBound; // Limite derecho del mapa
    public Transform leftBound;  // Limite izquierdo del mapa (opcional)

    Transform container;
    float direction = 1f;
    float startX;

    List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        container = new GameObject("EnemyContainer").transform;
        SpawnGrid();
        startX = container.position.x;
    }

    void SpawnGrid()
    {
        Camera cam = Camera.main;
        Vector2 topLeft = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, 0));
        Vector2 start = topLeft + new Vector2(spacing.x / 2, -spacing.y / 2);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Vector2 pos = start + new Vector2(c * spacing.x, -r * spacing.y);
                GameObject e = Instantiate(enemyPrefab, pos, Quaternion.identity, container);
                enemies.Add(e);
            }
        }

        container.position = Vector3.zero;
    }

    void Update()
    {
        float moveStep = speed * Time.deltaTime * direction;
        container.position += Vector3.right * moveStep;

        // Calcular los bordes reales de la cuadrícula
        float gridLeft = container.position.x + startX;
        float gridRight = gridLeft + (cols - 1) * spacing.x;

        // Rebote
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

        // Revisar si todos los enemigos están muertos
        enemies.RemoveAll(e => e == null);
        if (enemies.Count == 0)
        {
            FindObjectOfType<GameManager>()?.Win();
        }
    }
}