using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] 
    int _totalBulletNum;
    Queue<GameObject> _bulletPool = new Queue<GameObject>();
    BulletFactory _bulletFactory;

    // Start is called before the first frame update
    void Awake()
    {
        _bulletFactory = FindObjectOfType<BulletFactory>();

        for (int i = 0; i < _totalBulletNum; i++)
        {
            _bulletPool.Enqueue(_bulletFactory.CreateBullet());
        }
    }

    //void CreateBullet()
    //{
    //    //Create bullet
    //    GameObject bullet = Instantiate(_bulletPrefab, transform);
    //    bullet.SetActive(false);
    //    _bulletPool.Enqueue(bullet);
    //}

    public GameObject GetBullet(BulletType type)
    {
        if (_bulletPool.Count <= 1)
        {
            //in the stress point so create more bullet
            _bulletPool.Enqueue(_bulletFactory.CreateBullet());
        }
        GameObject bullet = _bulletPool.Dequeue();
        bullet.SetActive(true);

        switch (type)
        {
            case BulletType.PLAYER:
                //Set up player bullet.
                bullet.transform.eulerAngles = Vector3.zero;
                bullet.GetComponent<SpriteRenderer>().color = Color.white;
                bullet.tag = "PlayerBullet";
                break;
            case BulletType.ENEMY:
                //Set up player bullet.
                bullet.transform.eulerAngles = new Vector3(0, 0, 180);
                bullet.GetComponent<SpriteRenderer>().color = Color.green;
                bullet.tag = "EnemyBullet";
                break;
        }

        return bullet;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}




