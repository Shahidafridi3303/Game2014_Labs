using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    private GameManager gameManager;
    private SoundManager soundManager;

    [SerializeField]
    private int _score = 25;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Detect trigger collisions with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            soundManager.PlayScorePickup();
            gameManager.IncrementScore(_score);

            Destroy(gameObject);
        }
    }
}