using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class EnemyRedScrip : MonoBehaviour
{
    [SerializeField] private int scoreValue = 200;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed = 2f, direction = 1f;

    private void Awake()
    {
        StartCoroutine("FireRate");
    }

    // Update is called once per frame
    void Update()
    {
        float moveStep = speed * Time.deltaTime * direction;
        transform.position += Vector3.right * moveStep;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "LeftBound":
                direction *= -1f;
                break;
            case "RightBound":
                direction *= -1f;
                break;
        }
    }

    private void OnDestroy()
    {
        FindFirstObjectByType<GameManager>()?.AddScore(scoreValue);
    }

    private IEnumerator FireRate()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        StartCoroutine("FireRate");
    }
}
