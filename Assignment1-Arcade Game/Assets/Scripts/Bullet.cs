using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Reference to the GameManager script
    private GameManager gameManager;

    // Boundaries for the bullet
    [SerializeField] private Boundaries horizontalBoundary;
    [SerializeField] private Boundaries verticalBoundary;

    private void Start()
    {
        // Find the GameManager script in the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        CheckBoundaries();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Access the Animator component of the enemy
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
            Collider2D enemyCollider = collision.gameObject.GetComponent<Collider2D>();
            Rigidbody2D enemyRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            // Trigger the death animation
            enemyAnimator.SetTrigger("OnDie");

            // Disable enemy's collider
            enemyCollider.enabled = false;

            // Destroy the enemy after 0.4 seconds
            Destroy(collision.gameObject, 0.8f);

            // Increment the score in the GameManager
            gameManager.IncrementScore();

            // Destroy the bullet immediately
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
