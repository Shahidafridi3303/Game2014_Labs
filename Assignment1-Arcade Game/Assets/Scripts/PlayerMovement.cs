using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 3; //changing speed
    [SerializeField] Boundaries _horizontalBoundary, _verticalBoundary;
    [SerializeField] bool _isTestMobile;

    Camera _camera;
    Vector2 _destination;

    bool _isMobilePlatform = true;

    void Start()
    {
        _camera = Camera.main;

        if (!_isTestMobile)
        {
            _isMobilePlatform = Application.platform == RuntimePlatform.Android ||
                                Application.platform == RuntimePlatform.IPhonePlayer;
        }
    }

    void Update()
    {
        if (_isMobilePlatform)
        {
            GetTouchInput();
        }
        else
        {
            GetTraditionalInput();
        }

        CheckBoundaries();
    }

    void RotateTowards(Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - (Vector2)transform.position;
        transform.up = direction;
    }

    void Move()
    {
        transform.position = _destination;
    }

    void GetTraditionalInput()
    {
        float xAxis = Input.GetAxisRaw("Horizontal") * _movementSpeed * Time.deltaTime;
        float yAxis = Input.GetAxisRaw("Vertical") * _movementSpeed * Time.deltaTime;

        // Apply movement amount to transform
        _destination = new Vector3(xAxis + transform.position.x, yAxis + transform.position.y, 0);
        
        // Move
        Move();

        // Rotate
        RotateTowards(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void GetTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            _destination = _camera.ScreenToWorldPoint(touch.position);
            Vector2 _mouseClickLocation = _destination;
            _destination = Vector2.Lerp(transform.position, _destination, _movementSpeed * Time.deltaTime);

            // Move 
            Move();

            // Rotate
            RotateTowards(_mouseClickLocation);
        }
    }

    void CheckBoundaries()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, _horizontalBoundary.min, _horizontalBoundary.max);
        position.y = Mathf.Clamp(position.y, _verticalBoundary.min, _verticalBoundary.max);
        transform.position = position;
    }
}
