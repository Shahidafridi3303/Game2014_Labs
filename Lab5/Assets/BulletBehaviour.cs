using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour, IDamage
{
    Rigidbody2D _rigidbody;
    [SerializeField] float _speed = 10;
    [SerializeField] int _damage = 5;


    public int Damage()
    {
        return _damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
        Vector3 directionToTarget = (FindObjectOfType<PlayerBehavior>().transform.position - transform.position).normalized;
        _rigidbody.AddForce(directionToTarget * _speed, ForceMode2D.Impulse);
        Invoke("DestroyBullet", 1.5f);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
