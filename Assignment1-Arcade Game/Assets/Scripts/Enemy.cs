using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    // Player Awareness
    [SerializeField] private float _playerAwarenessDistance;
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }

    // Enemy Movement
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private Transform _player;
    private Rigidbody2D _rigidbody;
    private Vector2 _targetDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement>().transform; // Ensure player exists
    }

    private void Update()
    {
        if (_player != null)
        {
            UpdatePlayerAwareness();
        }
    }

    private void FixedUpdate()
    {
        if (AwareOfPlayer)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMovement();
        }
    }

    private void UpdatePlayerAwareness()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;
        AwareOfPlayer = enemyToPlayerVector.magnitude <= _playerAwarenessDistance;
    }

    private void MoveTowardsPlayer()
    {
        _targetDirection = DirectionToPlayer;
        RotateTowardsTarget();
        SetVelocity();
    }

    private void StopMovement()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    private void RotateTowardsTarget()
    {
        if (_targetDirection == Vector2.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, _targetDirection);
        Quaternion smoothedRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        _rigidbody.SetRotation(smoothedRotation);
    }

    private void SetVelocity()
    {
        _rigidbody.velocity = transform.up * _moveSpeed;
    }
}
