using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI newScoreText;
    [SerializeField] GameManager gameManager;
    [SerializeField] DataManager dataManager;

    public void Init()
    {
        scoreText.text = gameManager.Score.ToString();

        dataManager.Load();

        newScoreText.gameObject.SetActive(gameManager.HighScoreUpdated);

        if (dataManager.data.HighScore == 0)
        {
            highScoreText.text = "なし";
        }
        else
        {
            highScoreText.text = dataManager.data.HighScore.ToString();
        }
    }
}
