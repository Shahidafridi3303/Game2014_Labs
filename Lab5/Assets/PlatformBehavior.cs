using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.SetParent(null);
    }

}
