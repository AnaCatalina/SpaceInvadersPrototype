using UnityEngine;

public class EnemyBulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;         // Prefab de la bala enemiga que se instanciará.

    private float fireInterval = 5f;        // Tiempo promedio entre disparos.
    private float variance = 3f;            // Margen aleatorio para variar el intervalo de disparo.
    private float bulletSpeed = 3f;         // Velocidad con la que se moverán las balas.

    private float nextFire;                 // Marca de tiempo para el próximo disparo permitido.

    void Start()
    {
        ScheduleNext();                     // Calcula el primer momento de disparo.
    }

    void ScheduleNext()
    {
        // Define el tiempo exacto del próximo disparo con un valor aleatorio dentro del rango [intervalo - varianza, intervalo + varianza].
        nextFire = Time.time + fireInterval + Random.Range(-variance, variance);
    }

    void Update()
    {
        // Verifica si es momento de disparar según el reloj del juego.
        if (Time.time >= nextFire)
        {
            // Dispara solo con cierta probabilidad para evitar que todos los enemigos disparen simultáneamente.
            if (Random.value < 0.5f) // 50% de probabilidad de disparo.
            {
                GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // Crea la bala en la posición del enemigo.
                b.GetComponent<EnemyBullet>()?.Initialize(bulletSpeed);                            // Asigna la velocidad a la bala recién creada.
            }

            ScheduleNext(); // Programa el siguiente disparo.
        }
    }
}