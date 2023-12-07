using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Transform startCanvas;
    
    public Transform loseCanvas;
    public TextMeshProUGUI loseScoreText;
    public TextMeshProUGUI loseHighScoreText;

    public Transform highScoreCanvas;
    public TextMeshProUGUI highScoreText;

    private void Start()
    {
        GameplayManager.Instance.OnPlay += HideStartUI;
        GameplayManager.Instance.OnPlay += ShowHighScoreCanvas;
        GameplayManager.Instance.OnLose += Lose;
    }

    private void HideStartUI()
    {
        startCanvas.gameObject.SetActive(false);
    }
    
    private void Lose()
    {
        if (GameplayManager.Instance.score > GameplayManager.Instance.highScore)
        {
            GameplayManager.Instance.highScore = GameplayManager.Instance.score;
            PlayerPrefs.SetInt("HighScore", GameplayManager.Instance.highScore);
        }

        highScoreCanvas.gameObject.SetActive(false);

        loseScoreText.text = $"Score : {GameplayManager.Instance.score}";
        loseHighScoreText.text = $"HighScore : {GameplayManager.Instance.highScore}";
        loseCanvas.gameObject.SetActive(true);
    }

    private void ShowHighScoreCanvas()
    {
        highScoreText.text = $"HighScore : {GameplayManager.Instance.highScore}";
        highScoreCanvas.gameObject.SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}