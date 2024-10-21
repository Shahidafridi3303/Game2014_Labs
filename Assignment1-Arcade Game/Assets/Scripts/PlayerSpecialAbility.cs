using System.Collections;
using UnityEngine;

public class PlayerSpecialAbility : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 350f;
    [SerializeField] private GameObject circleSword;
    [SerializeField] private float activationInterval = 10f;
    [SerializeField] private float activeDuration = 3f;
    [SerializeField] private float damageInterval = 10f;

    private Coroutine damageCoroutine;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        circleSword.SetActive(false); // Make sure the sword is initially off
        StartCoroutine(ActivateSwordPeriodically());
    }

    private void Update()
    {
        RotateCircleSword();
    }

    private void RotateCircleSword()
    {
        circleSword.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private IEnumerator ActivateSwordPeriodically()
    {
        ActivateSword();
        yield return new WaitForSeconds(activeDuration);
        DeactivateSword();

        while (true)
        {
            yield return new WaitForSeconds(activationInterval);
            ActivateSword();

            yield return new WaitForSeconds(activeDuration);
            DeactivateSword();
        }
    }

    private void ActivateSword()
    {
        circleSword.SetActive(true);
    }

    private void DeactivateSword()
    {
        circleSword.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsSwordActive() && collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DealDamageOverTime(enemy));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DealDamageOverTime(Enemy enemy)
    {
        while (enemy != null && !enemy.isDead)
        {
            enemy.HandleDeath();
            gameManager.IncrementScore(5);

            yield return new WaitForSeconds(damageInterval);
        }

        damageCoroutine = null;
    }

    private bool IsSwordActive()
    {
        return circleSword.activeSelf;
    }
}