using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private int _healValue = 20;
    [SerializeField] private int _score = 25;
    [SerializeField] private float _boostFiringSpeed = 0.3f;
    [SerializeField] private float _boostFiringSpeed_Duration = 3.0f;

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
    }
}
