using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    GameObject _bulletPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        _bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform);
        bullet.SetActive(false);
        return bullet;
    }
}
