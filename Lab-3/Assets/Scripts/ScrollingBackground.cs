using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [SerializeField] Vector3 _spawnPosition;
    [SerializeField] private Boundaries _boundary;

    Vector3 _direction = Vector3.down;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        transform.position = transform.position + _direction * _speed * Time.deltaTime;

        if (transform.position.y < _boundary.min)
        {
            transform.position = _spawnPosition;
        }
    }
}
