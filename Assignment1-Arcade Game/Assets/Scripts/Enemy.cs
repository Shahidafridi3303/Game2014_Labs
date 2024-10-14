using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    // Detection radius for the enemy to sense the player
    [SerializeField] private float detectionRadius;
    // Speed at which the enemy moves towards the player
    [SerializeField] private float moveSpeed;

    private Transform playerTransform;
    private Rigidbody2D enemyRigidbody;

    private void Awake()
    {
        // Get the player's position and enemy's rigidbody
        playerTransform = FindObjectOfType<PlayerMovement>()?.transform;
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Check if player exists and is within detection range
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMovement();
        }
    }

    // Moves the enemy directly towards the player's position
    private void MoveTowardsPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        enemyRigidbody.velocity = direction * moveSpeed;
    }

    // Stops the enemy when the player is out of range
    private void StopMovement()
    {
        enemyRigidbody.velocity = Vector2.zero;
    }
}