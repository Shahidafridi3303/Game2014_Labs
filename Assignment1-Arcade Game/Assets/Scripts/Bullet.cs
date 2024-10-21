using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Boundaries horizontalBoundary;
    [SerializeField] private Boundaries verticalBoundary;
    private GameManager gameManager;

    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        CheckBoundaries();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().HandleDeath();
            gameManager.IncrementScore(5);
            BulletPool.Instance.ReturnBulletToPool(gameObject);
        }
    }

    private void CheckBoundaries()
    {
        Vector3 position = transform.position;
        if (IsOutOfBoundaries(position))
        {
            BulletPool.Instance.ReturnBulletToPool(gameObject);
        }
    }

    private bool IsOutOfBoundaries(Vector3 position)
    {
        return position.x < horizontalBoundary.min || position.x > horizontalBoundary.max ||
               position.y < verticalBoundary.min || position.y > verticalBoundary.max;
    }
}