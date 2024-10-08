using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    float _speed = 3;

    [SerializeField]
    Boundaries _boundry;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * _speed * Time.deltaTime;

        if (transform.position.y > _boundry.max || transform.position.y < _boundry.min)

            Destroy(gameObject);
    }
}
