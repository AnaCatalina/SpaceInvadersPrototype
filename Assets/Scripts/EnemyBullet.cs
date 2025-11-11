using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 3f;       // Velocidad interna de la bala (inicial por defecto).
    private float lifeTime = 6f;    // Tiempo de vida antes de autodestruirse.

    public void Initialize(float bulletSpeed)
    {
        speed = bulletSpeed;        // Permite asignar una velocidad diferente al crear la bala.
    }

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destruye automáticamente la bala después de 'lifeTime' segundos.
    }

    void Update()
    {
        // Mueve la bala hacia abajo cada frame según su velocidad.
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta colisión con el jugador.
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<GameManager>()?.LoseLife(); // Resta una vida al jugador usando el GameManager.
            Destroy(gameObject);                          // Destruye la bala tras impactar.
        }
    }
}