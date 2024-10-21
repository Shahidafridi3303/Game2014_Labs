using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject scoreTextObject;
    public GameObject finalScoreTextObject;

    private SoundManager soundManager;
    private int score = 0;
    private TMP_Text scoreText;
    private TMP_Text finalScoreText;


    void Start()
    {
        InitializeComponents();
        UpdateScoreText();
    }

    private void InitializeComponents()
    {
        soundManager = FindObjectOfType<SoundManager>();
        scoreText = scoreTextObject.GetComponent<TMP_Text>();
        finalScoreText = finalScoreTextObject.GetComponent<TMP_Text>();
    }

    public void IncrementScore(int increment)
    {
        score += increment;
        UpdateScoreText();
        soundManager.PlayEnemyKilled();
    }

    private void UpdateScoreText()
    {
        string scoreString = "Score: " + score.ToString();
        scoreText.text = scoreString;
        finalScoreText.text = scoreString;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }
}