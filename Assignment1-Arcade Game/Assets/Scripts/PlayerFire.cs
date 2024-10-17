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
    private float _timeBetweenShots = 0.67f;

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
        GameObject bullet = BulletPool.Instance.GetPooledBullet();

        if (bullet != null)
        {
            bullet.transform.position = _gunOffset.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);

            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            rigidbody.velocity = _bulletSpeed * transform.up;
        }
        else
        {
            Debug.LogWarning("No bullets available in the pool!");
        }
    }

    public void BoostFiringSpeed(float speed, float duration)
    {
        _timeBetweenShots = speed;
        StartCoroutine(ResetFireRate(duration));
    }

    IEnumerator ResetFireRate(float duration)
    {
        yield return new WaitForSeconds(duration);
        _timeBetweenShots = 0.67f;
    }
}
