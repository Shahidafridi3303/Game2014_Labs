using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject scoreTextObject;
    public GameObject FinalScoreTextObject;

    //public GameObject gameOverText;

    private int score = 0;
    private TMP_Text scoreText;
    private TMP_Text FinalScoreText;

    void Start()
    {
        scoreText = scoreTextObject.GetComponent<TMP_Text>();
        FinalScoreText = FinalScoreTextObject.GetComponent<TMP_Text>();

        scoreText.text = "Score: " + score.ToString();
        FinalScoreText.text = "Score: " + score.ToString();
    }

    public void IncrementScore(int _score)
    {
        score += _score;
        scoreText.text = "Score: " + score.ToString();
        FinalScoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        //gameOverText.SetActive(true);
        Time.timeScale = 0;
    }
}