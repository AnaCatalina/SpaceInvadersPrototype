using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    [SerializeField] public GameObject enemyRedPredab,enemyPrefab;
    [SerializeField] public int rows, cols;
    [SerializeField] public Vector2 spacing;
    [SerializeField] public float speed, descendAmount;
    [SerializeField] public Transform rightBound, leftBound, enemyRedSpawn, enemyRedSpawn2;

    public bool band = false;              // Bandera para controlar el cambio de dirección.
    Transform container;                    // Contenedor principal que agrupa a todos los enemigos.
    float direction = 1f;                   // Dirección actual del movimiento (1 = derecha, -1 = izquierda).
    float startX;                           // Posición inicial en X del contenedor.

    List<GameObject> enemies = new List<GameObject>(); // Lista para rastrear los enemigos activos.
    void Awake()
    {
        StartCoroutine("SpawnRedEnemy");
        // Inicializa singleton.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
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

        // Limpia la lista eliminando referencias nulas (enemigos destruidos).
        enemies.RemoveAll(e => e == null);
        switch(enemies.Count)
        {
            case 45:
                speed = 1f;
            break;

            case 30:
                speed = 1.5f;
            break;

            case 15:
                speed = 2f;
            break;

            case 1:
                speed = 4f;
            break;

            case 0:
                FindFirstObjectByType<GameManager>()?.Win();
            break;
        }
    }
    // Método estático llamado desde Enemy.cs. Redirige al singleton para modificar el estado del spawner real.
    public static void ChangeHight()
    {
        if (Instance == null) return;
        Instance.InternalChangeHight();
    }

    // Lógica de cambio de dirección y descenso — no estática.
    void InternalChangeHight()
    {
        direction *= -1f;
        if (container == null) container = new GameObject("EnemyContainer").transform; // Fallback por seguridad.
        container.position += Vector3.down * descendAmount;
    }
    private IEnumerator SpawnRedEnemy()
    {
        yield return new WaitForSeconds(10f);
        int random = UnityEngine.Random.Range(1, 2);
        switch(random)
        {
            case 1:
                GameObject r = Instantiate(enemyRedPredab, enemyRedSpawn2.position, Quaternion.identity);
                enemies.Add(r);
                break;
            case 2:
                GameObject r2 = Instantiate(enemyRedPredab, enemyRedSpawn.position, Quaternion.identity);
                enemies.Add(r2);
                break;
        }
        StartCoroutine("SpawnRedEnemy");
    }
}