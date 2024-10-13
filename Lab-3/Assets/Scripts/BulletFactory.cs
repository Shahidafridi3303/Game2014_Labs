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

    public GameObject CreateBullet(BulletType type)
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform);
        bullet.SetActive(false);

        switch (type)
        {
            case BulletType.PLAYER:
                //Set up player bullet.
                bullet.transform.eulerAngles = Vector3.zero;
                bullet.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/PlayerBullet");
                bullet.GetComponent<BulletBehavior>().SetType(BulletType.PLAYER);
                bullet.tag = "PlayerBullet";
                break;
            case BulletType.ENEMY:
                //Set up player bullet.
                bullet.transform.eulerAngles = new Vector3(0, 0, 180);
                bullet.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/EnemyBullet");
                bullet.GetComponent<BulletBehavior>().SetType(BulletType.ENEMY);
                bullet.tag = "EnemyBullet";
                break;
        }

        return bullet;
    }
}
