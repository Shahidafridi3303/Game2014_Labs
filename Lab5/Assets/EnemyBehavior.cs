using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    float _speed = 5;

    [SerializeField]
    Transform _baseCenterPoint;
    bool _IsGrounded;
    [SerializeField]
    float _groundCheckDistance;
    [SerializeField]
    Transform _frontGroundPoint;
    bool _IsThereAnyFrontStep;

    [SerializeField]
    Transform _frontObstaclePoint;
    bool _IsThereAnyFrontObstacle;

    [SerializeField]
    LayerMask _layerMask;

    private PlayerDetection _playerDetection;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _playerDetection = GetComponent<PlayerDetection>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _IsGrounded = Physics2D.Linecast(_baseCenterPoint.position, _baseCenterPoint.position + Vector3.down * _groundCheckDistance, _layerMask);
        _IsThereAnyFrontStep = Physics2D.Linecast(_baseCenterPoint.position, _frontGroundPoint.position, _layerMask);
        _IsThereAnyFrontObstacle = Physics2D.Linecast(_baseCenterPoint.position, _frontObstaclePoint.position, _layerMask);

        if (_IsGrounded && (!_IsThereAnyFrontStep || _IsThereAnyFrontObstacle))
        {
            ChangeDirection();
        }
        _animator.SetInteger("State", (int)AnimationStates.IDLE);
        if (_IsGrounded && !_playerDetection.GetLOSStatus())
            Move();
    }

    void Move()
    {
        _animator.SetInteger("State", (int)AnimationStates.RUN);
        transform.position += Vector3.left * transform.localScale.x * _speed;
    }

    void ChangeDirection()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(_baseCenterPoint.position, _baseCenterPoint.position + Vector3.down * _groundCheckDistance);
        Debug.DrawLine(_baseCenterPoint.position, _frontGroundPoint.position);
    }
}
