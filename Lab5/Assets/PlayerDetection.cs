using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField]
    bool _IsSensing;
    [SerializeField]
    bool _LOS;

    PlayerBehavior _player;
    [SerializeField]
    LayerMask _layerMask;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerBehavior>();
    }

    void Update()
    {
        if (_IsSensing)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, _player.transform.position, _layerMask);
            Vector2 playerDirection = _player.transform.position - transform.position;
            float playerDirectionValue = (playerDirection.x > 0) ? 1 : -1;
            float enemyLookingDirectionValue = (transform.localScale.x > 0) ? -1 : 1;

            _LOS = (hit.collider.name == "Player") && playerDirectionValue == enemyLookingDirectionValue;
        }
    }

    public bool GetLOSStatus()
    {
        return _LOS;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _IsSensing = true;
        }
    }

    private void OnDrawGizmos()
    {
        Color color = (_LOS) ? Color.green : Color.red;

        if (_IsSensing)
        {
            Debug.DrawLine(transform.position, _player.transform.position, color);
        }
    }
    
}
 