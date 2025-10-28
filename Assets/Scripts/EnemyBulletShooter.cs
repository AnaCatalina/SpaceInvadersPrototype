using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;

    private float fireInterval = 5f; // intervalo base
    private float variance = 3f;     // aleatoriedad
    private float bulletSpeed = 3f;  // velocidad de las balas

    private float nextFire;

    void Start()
    {
        ScheduleNext();
    }

    void ScheduleNext()
    {
        nextFire = Time.time + fireInterval + Random.Range(-variance, variance);
    }

    void Update()
    {
        if (Time.time >= nextFire)
        {
            // Solo dispara con cierta probabilidad para que no todos lo hagan al mismo tiempo
            if (Random.value < 0.5f) // 50% de probabilidad
            {
                GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                b.GetComponent<EnemyBullet>()?.Initialize(bulletSpeed);
            }

            ScheduleNext();
        }
    }
}