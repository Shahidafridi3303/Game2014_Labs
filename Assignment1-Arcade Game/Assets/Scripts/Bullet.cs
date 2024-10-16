using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Boundaries horizontalBoundary;
    [SerializeField] private Boundaries verticalBoundary;
    private GameManager gameManager;

    private void Start()
    {
        // Cache the reference to the GameManager for future use
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        CheckBoundaries();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hit an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Tell the enemy to handle its own destruction logic
            collision.gameObject.GetComponent<EnemyChase>().HandleDeath();

            // Increment the score in GameManager
            gameManager.IncrementScore(5);

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void CheckBoundaries()
    {
        Vector3 position = transform.position;
        if (position.x < horizontalBoundary.min || position.x > horizontalBoundary.max ||
            position.y < verticalBoundary.min || position.y > verticalBoundary.max)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}