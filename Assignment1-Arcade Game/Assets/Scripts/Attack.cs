using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageInterval = 1f; // Time interval between damage ticks

    private Coroutine damageCoroutine;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsPlayer(collision))
        {
            Health playerHealth = GetPlayerHealth(collision);
            if (playerHealth != null && damageCoroutine == null)
            {
                StartDamageCoroutine(playerHealth);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsPlayer(collision))
        {
            StopDamageCoroutine();
        }
    }

    private bool IsPlayer(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Player");
    }

    private Health GetPlayerHealth(Collider2D collision)
    {
        return collision.gameObject.GetComponent<Health>();
    }

    private void StartDamageCoroutine(Health health)
    {
        damageCoroutine = StartCoroutine(DealDamageOverTime(health));
    }

    private void StopDamageCoroutine()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
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