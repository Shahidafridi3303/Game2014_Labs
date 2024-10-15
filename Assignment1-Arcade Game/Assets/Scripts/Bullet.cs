using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Boundaries for the bullet
    [SerializeField] private Boundaries horizontalBoundary;
    [SerializeField] private Boundaries verticalBoundary;

    private void Update()
    {
        CheckBoundaries();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    // Check if the bullet exceeds the boundaries and destroy it if it does
    private void CheckBoundaries()
    {
        Vector3 position = transform.position;

        if (position.x < horizontalBoundary.min || position.x > horizontalBoundary.max ||
            position.y < verticalBoundary.min || position.y > verticalBoundary.max)
        {
            Destroy(gameObject);
        }
    }
}
