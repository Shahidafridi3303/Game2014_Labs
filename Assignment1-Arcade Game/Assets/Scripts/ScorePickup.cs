using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private int _score = 25;

    private void Start()
    {
        //Debug.Log("player detected");

        gameManager = FindObjectOfType<GameManager>();
    }

    // Detect trigger collisions with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.IncrementScore(_score);

            Destroy(gameObject);
        }
    }
}