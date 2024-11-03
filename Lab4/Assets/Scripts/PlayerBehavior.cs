using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    float _horizontalForce;
    [SerializeField]
    float _verticalForce;

    [SerializeField]
    float _horizontalSpeedLimit;
    [SerializeField]
    [Range(0f,  1f)]
    float _airFactor;

    Rigidbody2D _rigidbody;

    bool _isGrounded;
    [SerializeField]
    Transform _groundingPoint;
    [SerializeField]
    float _groundingRadius;
    [SerializeField]
    LayerMask _groundLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundingPoint.position, _groundingRadius, _groundLayerMask);

        Move();
        Jump();
    }

    void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        if (xInput != 0.0f)
        {
            Vector2 force = Vector2.right * xInput * _horizontalForce;
            if (!_isGrounded)
            {
                force *= _airFactor;
            }
            _rigidbody.AddForce(force);
            if (Mathf.Abs(_rigidbody.velocity.x) > _horizontalSpeedLimit)
            {
                float updatedXvalue = Mathf.Clamp(_rigidbody.velocity.x, _horizontalSpeedLimit, _horizontalSpeedLimit);
                _rigidbody.velocity = new Vector2(updatedXvalue, _rigidbody.velocity.y);
                //_rigidbody.velocity = new Vector2(Vector2.ClampMagnitude(_rigidbody.velocity, _horizontalSpeedLimit).x, _rigidbody.velocity.y);
            }
        }
    }

    void Jump()
    {
        var jumpPressed = Input.GetAxisRaw("Jump");

        if (_isGrounded && jumpPressed != 0.0f)
        {
            _rigidbody.AddForce(Vector2.up * _verticalForce);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundingPoint.position, _groundingRadius);
    }
}
