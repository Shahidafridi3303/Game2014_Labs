using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    PlayerDetection _playerDetection;
    [SerializeField] private int _fireDelay = 30;

    [SerializeField] private GameObject _bullet;
    bool _hasLOS;

    // Start is called before the first frame update
    void Start()
    {
        _playerDetection = GetComponent<PlayerDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        _hasLOS = _playerDetection.GetLOSStatus(); // Note: This line might have an issue in the tutorial. Ensure the method or property is correctly named.
    }

    private void FixedUpdate()
    {
        if (_hasLOS && Time.frameCount % _fireDelay == 0)
        {
            Execute(); // start range attack process
        }
    }

    public void Execute()
    {
        GameObject bullet = Instantiate<GameObject>(_bullet, transform.position, Quaternion.identity);
    }

}