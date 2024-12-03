using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    Animator _animator;
    Joystick _leftJoystick;
    private HealthBarController _healthBar;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    float _leftJoystickVerticalTreshold;

    [SerializeField] private float _deathltFallSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _healthBar = GetComponentInChildren<HealthBarController>();

        if (GameObject.Find("OnScreenControllers"))
        {
            _leftJoystick = GameObject.Find("LeftJoystick").GetComponent<Joystick>();
        }
    }

    void AnimatorStateControl()
    {
        if (_isGrounded)
        {
            if (Mathf.Abs(_rigidbody.velocity.x) > 0.2f)
            {
                _animator.SetInteger("State", (int)AnimationStates.RUN);
            }
            else
            {
                _animator.SetInteger("State", (int)AnimationStates.IDLE);
            }
        }
        else
        {
            if (Mathf.Abs(_rigidbody.velocity.y) > _deathltFallSpeed)
            {
                _animator.SetInteger("State", (int)AnimationStates.FALL);
            }
            else
            {
                _animator.SetInteger("State", (int)AnimationStates.JUMP);
            }
        }
    }

    void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        if (_leftJoystick)
        {
            xInput = _leftJoystick.Horizontal;
            //Debug.Log(_leftJoystick.Horizontal + " - " + _leftJoystick.Vertical);
        }

        if (xInput != 0.0f)
        {
            Vector2 force = Vector2.right * xInput * _horizontalForce;
            if (!_isGrounded)
            {
                force = new Vector2(force.x * _airFactor, force.y);
            }
            _rigidbody.AddForce(force);
            GetComponent<SpriteRenderer>().flipX = (force.x < 0.0f);
            if (Mathf.Abs(_rigidbody.velocity.x) > _horizontalSpeedLimit)
            {
                float updatedXvalue = Mathf.Clamp(_rigidbody.velocity.x,-_horizontalSpeedLimit, _horizontalSpeedLimit);
                _rigidbody.velocity = new Vector2(updatedXvalue, _rigidbody.velocity.y);
                //_rigidbody.velocity = new Vector2(Vector2.ClampMagnitude(_rigidbody.velocity, _horizontalSpeedLimit).x, _rigidbody.velocity.y);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundingPoint.position, _groundingRadius, _groundLayerMask);

        Move();
        Jump();
        AnimatorStateControl();
    }

    void Jump()
    {
        var jumpPressed = Input.GetAxisRaw("Jump");
        
        if (_leftJoystick)
        {
            jumpPressed = _leftJoystick.Vertical;
        }

        if (_isGrounded && jumpPressed > _leftJoystickVerticalTreshold)
        {
            _rigidbody.AddForce(Vector2.up * _verticalForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _healthBar.TakeDamage(collision.GetComponent<IDamage>().Damage());
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundingPoint.position, _groundingRadius);
    }
}
