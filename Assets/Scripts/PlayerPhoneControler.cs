using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class PlayerPhoneControler : MonoBehaviour
{    
    public float speed = 8f;              // Velocidad de desplazamiento horizontal del jugador.
    public GameObject bulletPrefab;       // Prefab de la bala que se instanciará al disparar.
    public Transform firePoint;           // Punto de origen del disparo (referencia a un transform en la escena).
    public float fireRate;        // Intervalo mínimo entre disparos.
    public float xLimit = 8f;             // Límite horizontal del área donde el jugador puede moverse.

    public bool band = true;
    public void TryFire()
    {
        if (band)
        {
            band = false;
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            StartCoroutine("FireRate");
        }
    }
    public void MovimientoDer()
    {
        float moveStep = speed * Time.deltaTime;
        Vector3 pos = transform.position + Vector3.right * moveStep;
        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        transform.position = pos;
    }

    public void MovimientoIzq()
    {
        float moveStep = speed * Time.deltaTime;
        Vector3 pos = transform.position + Vector3.left * moveStep;
        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        transform.position = pos;
    }
    private IEnumerator FireRate()
    {
        yield return new WaitForSeconds(0.25f);
        band = true;
    }

}
