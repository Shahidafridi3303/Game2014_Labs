using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform gunOffset;
    [SerializeField] private float timeBetweenShots = 0.67f;

    private SoundManager soundManager;
    private Coroutine autoFireCoroutine;

    void Start()
    {
        InitializeComponents();
        StartAutoFire();
    }

    private void InitializeComponents()
    {
        soundManager = GetComponent<SoundManager>();
    }

    private void StartAutoFire()
    {
        if (autoFireCoroutine == null)
        {
            autoFireCoroutine = StartCoroutine(AutoFire());
        }
    }

    private void StopAutoFire()
    {
        if (autoFireCoroutine != null)
        {
            StopCoroutine(autoFireCoroutine);
            autoFireCoroutine = null;
        }
    }

    private IEnumerator AutoFire()
    {
        while (true)
        {
            FireBullet();
            soundManager.PlayFireSound();
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    private void FireBullet()
    {
        GameObject bullet = BulletPool.Instance.GetPooledBullet();

        if (bullet != null)
        {
            SetBulletProperties(bullet);
        }
        else
        {
            Debug.LogWarning("No bullets available in the pool!");
        }
    }

    private void SetBulletProperties(GameObject bullet)
    {
        bullet.transform.position = gunOffset.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);

        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = bulletSpeed * transform.up;
    }

    public void BoostFiringSpeed(float newSpeed, float duration)
    {
        timeBetweenShots = newSpeed;
        StartCoroutine(ResetFireRateAfterDuration(duration));
    }

    private IEnumerator ResetFireRateAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        timeBetweenShots = 0.67f;
    }
}