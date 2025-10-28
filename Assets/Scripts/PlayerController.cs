using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 8f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.25f;
    public float xLimit = 8f;


    float nextFire;


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position;
        pos.x += h * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        transform.position = pos;


        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
        {
            TryFire();
        }
    }


    void TryFire()
    {
        if (Time.time >= nextFire)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
