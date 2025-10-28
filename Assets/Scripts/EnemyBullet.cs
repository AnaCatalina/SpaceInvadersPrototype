using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 3f; // velocidad interna más lenta
    private float lifeTime = 6f;

    public void Initialize(float bulletSpeed)
    {
        speed = bulletSpeed;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>()?.LoseLife();
            Destroy(gameObject);
        }
    }
}