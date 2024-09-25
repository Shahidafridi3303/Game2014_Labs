using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float _speed = 3; //changing speed
    [SerializeField] Boundaries _horizontalBoundary, _verticalBoundary;
    [SerializeField] bool _isTestMobile;

    Camera _camera;
    Vector2 _destination;

    bool _isMobilePlatform = true;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;

        if (!_isTestMobile)
        {
            _isMobilePlatform = Application.platform == RuntimePlatform.Android ||
                                Application.platform == RuntimePlatform.IPhonePlayer;
        }
    }

    // Update is called once per frame
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

        Move();

        CheckBoundaries();
    }

    void Move()
    {
        transform.position = _destination;
    }

    void GetTraditionalInput()
    {
        // get input and calculate movement amount
        float xAxis = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float yAxis = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;
        // Apply movement amount to transform
        _destination = new Vector3(xAxis + transform.position.x, yAxis + transform.position.y, 0);
    }

    void GetTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            _destination = _camera.ScreenToWorldPoint(touch.position);
            _destination = Vector2.Lerp(transform.position, _destination, _speed * Time.deltaTime);
        }
    }

    void CheckBoundaries()
    {
        //check if player pass the boundary
        if (transform.position.x > _horizontalBoundary.max)
        {
            transform.position = new Vector3(_horizontalBoundary.min, transform.position.y, 0);
        }
        else if (transform.position.x < _horizontalBoundary.min)
        {
            transform.position = new Vector3(_horizontalBoundary.max, transform.position.y, 0);
        }

        if (transform.position.y > _verticalBoundary.max)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundary.max, 0);
        }
        else if (transform.position.y < _verticalBoundary.min)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundary.min, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ohh Noo! We got hit Captain!!!");
    }
}
