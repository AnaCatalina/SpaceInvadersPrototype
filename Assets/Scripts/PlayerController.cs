using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;              // Velocidad de desplazamiento horizontal del jugador.
    public GameObject bulletPrefab;       // Prefab de la bala que se instanciará al disparar.
    public Transform firePoint;           // Punto de origen del disparo (referencia a un transform en la escena).
    public float fireRate = 0.25f;        // Intervalo mínimo entre disparos.
    public float xLimit = 8f;             // Límite horizontal del área donde el jugador puede moverse.

    float nextFire;                       // Marca de tiempo para controlar la cadencia de disparo.

    void Update()
    {
        // Movimiento horizontal del jugador.
        float h = Input.GetAxis("Horizontal");            // Captura el valor del eje horizontal (-1 a 1).
        Vector3 pos = transform.position;                 // Copia la posición actual.
        pos.x += h * speed * Time.deltaTime;              // Modifica la posición en X según la entrada y la velocidad.
        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);      // Restringe la posición dentro de los límites.
        transform.position = pos;                         // Aplica la nueva posición al jugador.

        // Control de disparo (barra espaciadora o botón de disparo).
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
        {
            TryFire();                                    // Intenta disparar si el tiempo lo permite.
        }
    }

    void TryFire()
    {
        // Verifica si ha pasado el tiempo suficiente desde el último disparo.
        if (Time.time >= nextFire)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // Crea una bala en la posición del firePoint.
            nextFire = Time.time + fireRate;                                    // Calcula el próximo momento de disparo permitido.
        }
    }
}