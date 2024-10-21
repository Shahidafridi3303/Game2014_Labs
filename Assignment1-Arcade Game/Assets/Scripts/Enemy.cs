using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float playerDetectionRadius = 2.0f;
    [SerializeField] private float chaseSpeed = 1.0f;
    [SerializeField] private GameObject healthPickupPrefab, scorePickupPrefab, speedBoostPrefab, shieldPrefab;
    [SerializeField] private float healthSpawnChance = 0.2f; // chance to spawn health pickup
    [SerializeField] private float scoreSpawnChance = 0.3f; // chance to spawn score pickup
    [SerializeField] private bool isLargeEnemy = false; // Flag to determine if the enemy is large

    // Boundaries for the enemy's movement
    [SerializeField] private Boundaries horizontalBoundary, verticalBoundary;

    private Transform playerTransform;
    private Rigidbody2D enemyRigidbody;
    private Vector2 targetDirection;
    public bool isDead = false;

    private Animator enemyAnimator;
    private Collider2D enemyCollider;
    private EnemyHeal health;

    private void Awake()
    {
        InitializeComponents();
        InitializeMovement();
        AdjustForLargeEnemy();
    }

    private void InitializeComponents()
    {
        playerTransform = FindObjectOfType<PlayerMovement>()?.transform;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        health = GetComponent<EnemyHeal>();
    }

    private void InitializeMovement()
    {
        targetDirection = GetRandomDirection();
        enemyRigidbody.velocity = targetDirection * chaseSpeed;
    }

    private void AdjustForLargeEnemy()
    {
        if (isLargeEnemy)
        {
            transform.localScale *= 3;
            health.ResetHealth();
        }
    }

    private void OnEnable()
    {
        ResetEnemy();
    }

    private void ResetEnemy()
    {
        isDead = false;
        enemyCollider.enabled = true;
        InitializeMovement();
        health.ResetHealth();
    }

    private void Update()
    {
        if (isDead) return;

        if (IsPlayerInRange())
        {
            MoveTowardsPlayer();
        }
        else
        {
            ContinueCurrentDirection();
        }

        EnforceBounds();
    }

    private bool IsPlayerInRange()
    {
        return playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= playerDetectionRadius;
    }

    private void MoveTowardsPlayer()
    {
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        targetDirection = directionToPlayer;
        enemyRigidbody.velocity = directionToPlayer * chaseSpeed;
    }

    private void ContinueCurrentDirection()
    {
        enemyRigidbody.velocity = targetDirection * chaseSpeed;
    }

    private void EnforceBounds()
    {
        Vector2 pos = transform.position;

        if (pos.x < horizontalBoundary.min || pos.x > horizontalBoundary.max)
        {
            pos.x = Mathf.Clamp(pos.x, horizontalBoundary.min, horizontalBoundary.max);
            BounceDirectionX();
        }

        if (pos.y < verticalBoundary.min || pos.y > verticalBoundary.max)
        {
            pos.y = Mathf.Clamp(pos.y, verticalBoundary.min, verticalBoundary.max);
            BounceDirectionY();
        }

        transform.position = pos;
    }

    private void BounceDirectionX()
    {
        targetDirection.x = -targetDirection.x;
        enemyRigidbody.velocity = targetDirection * chaseSpeed;
    }

    private void BounceDirectionY()
    {
        targetDirection.y = -targetDirection.y;
        enemyRigidbody.velocity = targetDirection * chaseSpeed;
    }

    private Vector2 GetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        return new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
    }

    public void HandleDeath()
    {
        health.currentHealth--;
        isDead = health.currentHealth == 0;

        if (isDead)
        {
            enemyAnimator.SetTrigger("OnDie");
            enemyCollider.enabled = false;
            enemyRigidbody.velocity = Vector2.zero;
            
            StartCoroutine(SpawnPickupsAfterDeath());
        }
    }

    private IEnumerator SpawnPickupsAfterDeath()
    {
        yield return new WaitForSeconds(0.8f);

        if (isLargeEnemy)
        {
            SpawnLargeEnemyPickup();
        }
        else
        {
            SpawnRegularEnemyPickup();
        }

        EnemyPool.Instance.ReturnEnemyToPool(gameObject);
        health.HandleDeath();
    }

    private void SpawnLargeEnemyPickup()
    {
        int speedBoostOrShield = Random.Range(0, 2);
        if (speedBoostOrShield == 0)
        {
            Instantiate(speedBoostPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        }
    }

    private void SpawnRegularEnemyPickup()
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue < healthSpawnChance)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }
        else if (randomValue < healthSpawnChance + scoreSpawnChance)
        {
            int scoreOrSpeed = Random.Range(0, 2);
            if (scoreOrSpeed == 0)
            {
                Instantiate(scorePickupPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(speedBoostPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}