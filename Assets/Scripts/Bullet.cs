using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;          // Velocidad de movimiento de la bala.
    public float lifeTime = 4f;        // Tiempo de vida antes de que se destruya automáticamente.

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destruye la bala después de 'lifeTime' segundos para evitar acumular objetos en la escena.
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); // Mueve la bala hacia arriba constantemente a la velocidad definida.
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Se ejecuta cuando la bala entra en contacto con otro collider que tenga el modo "Is Trigger" activado.
        if (other.CompareTag("Enemy"))                    // Comprueba si el objeto con el que colisionó tiene la etiqueta "Enemy".
        {
            other.GetComponent<Enemy>()?.Die();           // Busca el componente 'Enemy' en ese objeto y llama a su método 'Die()' (si existe).
            Destroy(gameObject);                          // Destruye la bala inmediatamente después del impacto.
        }
    }
}