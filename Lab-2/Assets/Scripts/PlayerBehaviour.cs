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
        // get input and calculate movement amount
        float xAxis = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float yAxis = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;
        // Apply movement amount to transform

        transform.position += new Vector3(xAxis, yAxis, 0);

        //check if player pass the boundary
        if (transform.position.x > _horizontalBoundary.max)
        {
            transform.position = new Vector3(_horizontalBoundary.min, transform.position.y, 0);
        }
        else if (transform.position.x < _horizontalBoundary.min)
        {
            transform.position = new Vector3(_horizontalBoundary.max, transform.position.y, 0);
        }

        if (transform.position.y > _verticalBoundary.max)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundary.max, 0);
        }
        else if (transform.position.y < _verticalBoundary.min)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundary.min, 0);
        }
    }
}
