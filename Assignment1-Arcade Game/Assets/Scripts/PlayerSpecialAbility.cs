using System.Collections;
using UnityEngine;

public class PlayerSpecialAbility : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 350f;
    [SerializeField] private GameObject _circleSword;
    [SerializeField] private float _activationInterval = 10f;
    [SerializeField] private float _activeDuration = 3f;
    [SerializeField] private float damageInterval = 1f; // Time interval between damage ticks

    private Coroutine damageCoroutine;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(ActivateSwordPeriodically());
    }

    private void Update()
    {
        _circleSword.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private IEnumerator ActivateSwordPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(_activationInterval);
            _circleSword.SetActive(true);

            yield return new WaitForSeconds(_activeDuration);
            _circleSword.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_circleSword.activeSelf && collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // Only start the coroutine if not already running
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
            // Stop the coroutine when the enemy exits the collider
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

            // Increment the score in GameManager
            gameManager.IncrementScore(5);

            yield return new WaitForSeconds(damageInterval);
        }

        damageCoroutine = null; 
    }
}
