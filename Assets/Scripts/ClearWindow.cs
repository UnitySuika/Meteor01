using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] GameManager gameManager;
    [SerializeField] DataManager dataManager;

    public void Init()
    {
        scoreText.text = gameManager.Score.ToString();

        dataManager.Load();

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
