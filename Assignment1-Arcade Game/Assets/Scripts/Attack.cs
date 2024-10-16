using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageInterval = 1f; // Time interval between damage ticks

    private Coroutine damageCoroutine;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null && damageCoroutine == null)
            {
                // Start the coroutine to deal damage at intervals
                damageCoroutine = StartCoroutine(DealDamageOverTime(health));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop the coroutine when the collision ends
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DealDamageOverTime(Health health)
    {
        while (true)
        {
            health.TakeDamage(damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}