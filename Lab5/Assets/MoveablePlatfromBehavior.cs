using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatfromBehavior : MonoBehaviour
{
    [SerializeField]
    PlatformMovementTypes _type;

    [SerializeField] float _horizontalSpeed = 5;
    [SerializeField] float _horizontalDistance = 5;
    [SerializeField] float _verticalSpeed = 5;
    [SerializeField] float _verticalDistance = 5;

    [SerializeField]
    List<Transform> _pathList = new List<Transform>();

    List<Vector2> _destinations = new List<Vector2>();
    private int _destinationIndex = 0;

    Vector2 _startPosition;
    Vector2 _endPosition;
    float _timer;
    [Range(0f, 0.1f)]
    [SerializeField] float _CustomMovementTimeChangeFactor;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in _pathList)
        {
            _destinations.Add(t.position);
        }

        _destinations.Add(transform.position);

        _startPosition = transform.position;
        _endPosition = _destinations[0];
    }

    private void FixedUpdate()
    {
        if (_type == PlatformMovementTypes.CUSTOM)
        {
            if (_timer >= 1)
            {
                _timer = 0;
                _destinationIndex++;
                if (_destinationIndex >= _destinations.Count)
                {
                    _destinationIndex = 0;
                }

                _startPosition = transform.position;
                _endPosition = _destinations[_destinationIndex];
            }
            else
            {
                {
                    _timer += _CustomMovementTimeChangeFactor;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        switch (_type)
        {
            case PlatformMovementTypes.HORIZONTAL:
                transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance) + _startPosition.x, transform.position.y);
                break;
            case PlatformMovementTypes.VERTICAL:
                transform.position = new Vector2(transform.position.x, Mathf.PingPong(_verticalSpeed * Time.time, _verticalDistance) + _startPosition.y);
                break;
            case PlatformMovementTypes.DIAGONAL_RIGHT:
                transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance) + _startPosition.x,
                                                    Mathf.PingPong(_verticalSpeed * Time.time, _verticalDistance) + _startPosition.y);
                break;
            case PlatformMovementTypes.DIAGONAL_LEFT:
                transform.position = new Vector2(_startPosition.x - Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance),
                                                     Mathf.PingPong(_verticalSpeed * Time.time, _verticalDistance) + _startPosition.y);
                break;
            case PlatformMovementTypes.CUSTOM:
                transform.position = Vector2.Lerp(_startPosition, _endPosition, _timer);
                break;
        }
    }
}