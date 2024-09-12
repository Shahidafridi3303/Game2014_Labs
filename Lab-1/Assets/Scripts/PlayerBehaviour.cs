using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float _speed = 3;

    [SerializeField] Boundaries _horizontalBoundary, _verticalBoundary;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float yAxis = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;

        transform.position += new Vector3(xAxis, yAxis, 0);

        if (transform.position.x > _horizontalBoundary.max)
        {
            transform.position = new Vector3(_horizontalBoundary.max, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < _horizontalBoundary.min)
        {
            transform.position = new Vector3(_horizontalBoundary.min, transform.position.y, transform.position.z);
        }

        if (transform.position.x > _verticalBoundary.max)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundary.max, transform.position.z);
        }
        else if (transform.position.x < _horizontalBoundary.min)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundary.min, transform.position.z);
        }
    }
}
