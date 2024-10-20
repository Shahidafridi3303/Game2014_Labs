using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private int _healValue = 20;
    [SerializeField] private int _score = 25;
    [SerializeField] private float _boostFiringSpeed = 0.3f;
    [SerializeField] private float _boostFiringSpeed_Duration = 3.0f;
    [SerializeField] private GameObject _shield;

    private GameManager gameManager;
    private SoundManager soundManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealthPickup"))
        {
            GetComponent<Health>().AddHealth(_healValue);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("ScorePickup"))
        {
            soundManager.PlayScorePickup();
            gameManager.IncrementScore(_score);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("SpeedBoost"))
        {
            soundManager.PlayScorePickup();
            GetComponent<PlayerFire>().BoostFiringSpeed(_boostFiringSpeed, _boostFiringSpeed_Duration);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            soundManager.PlayHealthPickup();
            _shield.SetActive(true); // Activate the shield
            StartCoroutine(DisableDamageForDuration(5.0f)); // Disable damage for 5 seconds
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator DisableDamageForDuration(float duration)
    {
        Health health = GetComponent<Health>();
        health.CanTakeDamage = false; // Disable taking damage
        yield return new WaitForSeconds(duration);
        health.CanTakeDamage = true; // Re-enable taking damage
        _shield.SetActive(false); // Deactivate the shield
    }
}
