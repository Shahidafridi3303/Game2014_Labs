using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float _speed = 3;


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
    }
}
