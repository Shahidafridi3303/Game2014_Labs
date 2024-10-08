using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    float _speed = 3;
    float _baseSpeed = 3;

    [SerializeField]
    Boundaries _boundry;

    private BulletManager _bulletManager;

    // Start is called before the first frame update
    void Start()
    {
        _baseSpeed = _speed;
        _bulletManager = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * _speed * Time.deltaTime;

        if (transform.position.y > _boundry.max || transform.position.y < _boundry.min)
        {
            //Destroy(gameObject);
            _bulletManager.ReturnBullet(this.gameObject);
        }
    }

    public void RelativeSpeedAddision(float speed)
    {
        _speed = _baseSpeed + speed;
    }
}
