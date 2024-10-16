using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private Transform _gunOffset;

    [SerializeField]
    private float _timeBetweenShots;

    private SoundManager _soundManager;

    private Coroutine _autoFireCoroutine;

    void Start()
    {
        _soundManager = GetComponent<SoundManager>();
        StartAutoFire();
    }

    void StartAutoFire()
    {
        if (_autoFireCoroutine == null)
        {
            _autoFireCoroutine = StartCoroutine(AutoFire());
        }
    }

    void StopAutoFire()
    {
        if (_autoFireCoroutine != null)
        {
            StopCoroutine(_autoFireCoroutine);
            _autoFireCoroutine = null;
        }
    }

    IEnumerator AutoFire()
    {
        while (true)
        {
            FireBullet();
            _soundManager.PlayFireSound();
            yield return new WaitForSeconds(_timeBetweenShots);
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.velocity = _bulletSpeed * transform.up;
    }
}
