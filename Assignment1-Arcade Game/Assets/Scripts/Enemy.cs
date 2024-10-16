using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float playerDetectionRadius = 2.0f;
    [SerializeField] private float chaseSpeed = 1.0f;
    [SerializeField] private GameObject healthPickupPrefab;
    [SerializeField] private GameObject scorePickupPrefab;
    [SerializeField] private float healthSpawnChance = 0.2f; // chance to spawn health pickup
    [SerializeField] private float scoreSpawnChance = 0.3f; // chance to spawn health pickup

    // Boundaries for the enemy's movement
    [SerializeField] private Boundaries horizontalBoundary, verticalBoundary;

    private Transform playerTransform;
    private Rigidbody2D enemyRigidbody;
    private Vector2 targetDirection;
    private bool isDead = false;

    private Animator enemyAnimator;
    private Collider2D enemyCollider;

    private void Awake()
    {
        // Get the player's transform and the enemy's Rigidbody2D component
        playerTransform = FindObjectOfType<PlayerMovement>()?.transform;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();

        // Initialize enemy movement in a random forward direction
        targetDirection = GetRandomDirection();
        enemyRigidbody.velocity = targetDirection * chaseSpeed;
    }

    private void OnEnable()
    {
        isDead = false;
        enemyCollider.enabled = true;
        targetDirection = GetRandomDirection();
        enemyRigidbody.velocity = targetDirection * chaseSpeed;
    }

    private void Update()
    {
        if (isDead) return;

        // Check if the player exists and is within detection range
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= playerDetectionRadius)
        {
            MoveTowardsPlayer(); // Chase the player if in range
        }
        else
        {
            // Continue moving in the current direction if the player is out of range
            enemyRigidbody.velocity = targetDirection * chaseSpeed;
        }

        EnforceBounds();  // Ensure the enemy stays within the defined boundaries
    }

    // Moves the enemy directly towards the player's position
    private void MoveTowardsPlayer()
    {
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        targetDirection = directionToPlayer;
        enemyRigidbody.velocity = directionToPlayer * chaseSpeed;
    }

    // Ensure the enemy stays within defined boundaries using the Boundaries struct
    private void EnforceBounds()
    {
        Vector2 pos = transform.position;

        // Enforce horizontal boundaries
        if (pos.x < horizontalBoundary.min)
        {
            pos.x = horizontalBoundary.min;
            BounceDirectionX();
        }
        else if (pos.x > horizontalBoundary.max)
        {
            pos.x = horizontalBoundary.max;
            BounceDirectionX();
        }

        // Enforce vertical boundaries
        if (pos.y < verticalBoundary.min)
        {
            pos.y = verticalBoundary.min;
            BounceDirectionY();
        }
        else if (pos.y > verticalBoundary.max)
        {
            pos.y = verticalBoundary.max;
            BounceDirectionY();
        }

        transform.position = pos; // Update the enemy's position after enforcing boundaries
    }

    // Reverse the horizontal movement direction upon hitting a boundary
    private void BounceDirectionX()
    {
        targetDirection.x = -targetDirection.x;
        enemyRigidbody.velocity = targetDirection * chaseSpeed;
    }

    // Reverse the vertical movement direction upon hitting a boundary
    private void BounceDirectionY()
    {
        targetDirection.y = -targetDirection.y;
        enemyRigidbody.velocity = targetDirection * chaseSpeed;
    }

    // Get a random initial direction for the enemy
    private Vector2 GetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        return new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
    }

    public void HandleDeath()
    {
        if (isDead) return;

        isDead = true;

        enemyAnimator.SetTrigger("OnDie");
        enemyCollider.enabled = false;
        enemyRigidbody.velocity = Vector2.zero;

        StartCoroutine(SpawnHealthOrScorePickupAfterDelay());
    }

    // Coroutine to spawn a health or score pickup after a delay
    private IEnumerator SpawnHealthOrScorePickupAfterDelay()
    {
        yield return new WaitForSeconds(0.8f);

        float randomValue = Random.Range(0f, 1f);
        if (randomValue < healthSpawnChance)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }
        else if (randomValue < healthSpawnChance + scoreSpawnChance)
        {
            Instantiate(scorePickupPrefab, transform.position, Quaternion.identity);
        }

        // Instead of destroying the enemy, return it to the pool
        EnemyPool.Instance.ReturnEnemyToPool(gameObject);
    }
}
