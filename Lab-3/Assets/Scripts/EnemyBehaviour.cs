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

    SpriteRenderer _spriteRenderer;

    Color[] _colors = { Color.green, Color.cyan, Color.white, Color.magenta, Color.gray };

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        //Move enemy vertically and horizontally
        transform.position = new Vector2(Mathf.PingPong(_horizontalspeed * Time.time, _horizontalBoundry.max - _horizontalBoundry.min) + _horizontalBoundry.min
            /*transform. position. x + _horizontalSpeed * Time. deltaTime*/
            , transform.position.y + _verticalspeed * Time.deltaTime);

        //check if player off the sceen from bottom, if yes, reset enemy
        if (transform.position.y < _verticalBoundry.min)
        {
            Reset();
        }

        // checks if player off the screen from sides, if yes, change horizontal speed to other direction
        //if (transform.position.x > _horizontalBoundry.max || transform.position.x < _horizontalBoundry.min)
        //{
        //    _horizontalspeed = -_horizontalspeed;
        //}
    }

    public IEnumerator DyingRoutime()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.2f);
        _spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void Reset() // it reset the enemy's position and speed
    {
        _spriteRenderer.color = _colors[Random.Range(0, _colors.Length)];
        _spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        transform.position = new Vector2(Random.Range(_horizontalBoundry.min, _horizontalBoundry.max), _verticalBoundry.max);
        transform.localScale = new Vector3(1f + Random.Range(-.3f, .3f), 1f + Random.Range(-.3f, .3f), 1f);
        _verticalspeed = Random.Range(_verticalSpeedRange.min, _verticalSpeedRange.max);
        _horizontalspeed = Random.Range(_horizontalSpeedRange.min, _horizontalSpeedRange.max);
    }
}
