using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;              // Velocidad de desplazamiento horizontal del jugador.
    public GameObject bulletPrefab;       // Prefab de la bala que se instanciará al disparar.
    public Transform firePoint;           // Punto de origen del disparo (referencia a un transform en la escena).
    public float fireRate = 0.25f;        // Intervalo mínimo entre disparos.
    public float xLimit = 8f;             // Límite horizontal del área donde el jugador puede moverse.

    float nextFire;                       // Marca de tiempo para controlar la cadencia de disparo.

    private void Awake()
    {
        StartCoroutine("FireRate");
    }
    void Update()
    {
        // Lectura por teclado/joystick (sigue funcionando).
        float h = Input.GetAxis("Horizontal");

        // --- Entrada táctil: mantiene movimiento mientras se mantiene el dedo en la mitad izquierda/derecha ---
        if (Input.touchCount > 0)
        {
            bool leftPressed = false;
            bool rightPressed = false;

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) continue;

                // Ignora toques sobre UI (si hay EventSystem en la escena).
                if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(t.fingerId)) continue;

                if (t.position.x < Screen.width * 0.5f) leftPressed = true;
                else rightPressed = true;
            }

            if (leftPressed && !rightPressed) h = -1f;
            else if (rightPressed && !leftPressed) h = 1f;
            else h = 0f; // ambos presionados -> sin movimiento
        }
        // --- Soporte para testing en Editor: click/hold con ratón en pantalla ---
        else if (Input.GetMouseButton(0))
        {
            // Ignora clicks sobre UI
            if (EventSystem.current == null || !EventSystem.current.IsPointerOverGameObject())
            {
                Vector2 mp = Input.mousePosition;
                h = (mp.x < Screen.width * 0.5f) ? -1f : 1f;
            }
        }

        // Aplicar movimiento
        Vector3 pos = transform.position;
        pos.x += h * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        transform.position = pos;
    }

    public void TryFire()
    {
        // Verifica si ha pasado el tiempo suficiente desde el último disparo.
        if (Time.time >= nextFire)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // Crea una bala en la posición del firePoint.
            nextFire = Time.time + fireRate;                                    // Calcula el próximo momento de disparo permitido.
        }
        StartCoroutine("FireRate");
    }
    private IEnumerator FireRate()
    {
        yield return new WaitForSeconds(0.75f);
        TryFire();
    }
}