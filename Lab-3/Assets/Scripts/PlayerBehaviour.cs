using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float _speed = 3; //changing speed
    [SerializeField] Boundaries _horizontalBoundary, _verticalBoundary;
    [SerializeField] bool _isTestMobile;
    [SerializeField] [Range(0.01f, 1.00f)] float _shootingCoolDownTime;
    [SerializeField] Transform _shootingPoint;
    Camera _camera;
    Vector2 _destination;

    GameController _gamecontroller;
    BulletManager _bulletManager;

    //GameObject _bulletPrefab;

    bool _isMobilePlatform = true;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _gamecontroller = FindObjectOfType<GameController>();
        _bulletManager = FindObjectOfType<BulletManager>();
        //_bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        if (!_isTestMobile)
        {
            _isMobilePlatform = Application.platform == RuntimePlatform.Android ||
                                Application.platform == RuntimePlatform.IPhonePlayer;
        }

        StartCoroutine(ShootingRoutine());
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

    IEnumerator ShootingRoutine()
    {
        // Instantiate(_bulletPrefab, _shootingPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(_shootingCoolDownTime);
        GameObject bullet = _bulletManager.GetBullet(BulletType.PLAYER);
        bullet.transform.position = _shootingPoint.position;
        StartCoroutine(ShootingRoutine());
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
        if (collision.CompareTag("Enemy"))
        {
           _gamecontroller.ChangeScore(-9);
           //Destroy(collision.gameObject);
           //collision.gameObject.SetActive(false);
           StartCoroutine(collision.GetComponent<EnemyBehaviour>().DyingRoutime());
        }

        if (collision.CompareTag("EnemyBullet"))
        {
            _gamecontroller.ChangeScore(-5);
        }
    }
}
