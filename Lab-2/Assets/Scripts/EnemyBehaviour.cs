using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] Boundaries _verticalSpeedRange;
    [SerializeField] Boundaries _horizontalSpeedRange;

    float _verticalspeed;
    float _horizontalspeed;

    [SerializeField] Boundaries _verticalBoundry;
    [SerializeField] Boundaries _horizontalBoundry;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        // Move enemy vertically and horizontally
        transform.position = new Vector2(transform.position.x + _horizontalspeed * Time.deltaTime, transform.position.y + _verticalspeed * Time.deltaTime);

        //check if player off the sceen from bottom, if yes, reset enemy
        if (transform.position.y < _verticalBoundry.min)
        {
            Reset();
        }

        // checks if player off the screen from sides, if yes, change horizontal speed to otther direction
        if (transform.position.x > _horizontalBoundry.max || transform.position.x < _horizontalBoundry.min)
        {
            _horizontalspeed = -_horizontalspeed;
        }
    }

    private void Reset() // it reset the enmy's position and speed
    {
        transform.position = new Vector2(Random.Range(_horizontalBoundry.min, _horizontalBoundry.max), _verticalBoundry.max);
        _verticalspeed = Random.Range(_verticalSpeedRange.min, _verticalSpeedRange.max);
        _horizontalspeed = Random.Range(_horizontalSpeedRange.min, _horizontalSpeedRange.max);
    }
}
