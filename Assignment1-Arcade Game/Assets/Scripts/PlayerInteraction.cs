using System.Collections;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private int healValue = 20;
    [SerializeField] private int scoreValue = 25;
    [SerializeField] private float boostFiringSpeed = 0.3f;
    [SerializeField] private float boostFiringSpeedDuration = 3.0f;
    [SerializeField] private GameObject shield;

    private GameManager gameManager;
    private SoundManager soundManager;

    private void Start()
    {
        InitializeManagers();
    }

    private void InitializeManagers()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealthPickup"))
        {
            HandleHealthPickup(collision);
        }
        else if (collision.gameObject.CompareTag("ScorePickup"))
        {
            HandleScorePickup(collision);
        }
        else if (collision.gameObject.CompareTag("SpeedBoost"))
        {
            HandleSpeedBoost(collision);
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            HandleShieldPickup(collision);
        }
    }

    private void HandleHealthPickup(Collider2D collision)
    {
        GetComponent<Health>().AddHealth(healValue);
        Destroy(collision.gameObject);
    }

    private void HandleScorePickup(Collider2D collision)
    {
        soundManager.PlayScorePickup();
        gameManager.IncrementScore(scoreValue);
        Destroy(collision.gameObject);
    }

    private void HandleSpeedBoost(Collider2D collision)
    {
        soundManager.PlayScorePickup();
        GetComponent<PlayerFire>().BoostFiringSpeed(boostFiringSpeed, boostFiringSpeedDuration);
        Destroy(collision.gameObject);
    }

    private void HandleShieldPickup(Collider2D collision)
    {
        soundManager.PlayHealthPickup();
        ActivateShield();
        StartCoroutine(DisableDamageTemporarily(5.0f));
        Destroy(collision.gameObject);
    }

    private void ActivateShield()
    {
        shield.SetActive(true);
    }

    private IEnumerator DisableDamageTemporarily(float duration)
    {
        Health health = GetComponent<Health>();
        health.CanTakeDamage = false; // Disable taking damage
        yield return new WaitForSeconds(duration);
        health.CanTakeDamage = true; // Re-enable taking damage
        shield.SetActive(false); // Deactivate the shield
    }
}