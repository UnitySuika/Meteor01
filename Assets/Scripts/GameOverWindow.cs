using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI highTimeText;
    [SerializeField] ScheduleManager scheduleManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] DataManager dataManager;

    public void Init()
    {
        ScheduleManager.TimeData timeData = scheduleManager.GetTime();
        string time = "";
        time += timeData.Day.ToString() + "日 ";
        time += timeData.Hour.ToString("d2") + ":";
        time += timeData.Min.ToString("d2");
        timeText.text = time;

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

        if (dataManager.data.HighTime == null)
        {
            highTimeText.text = "なし";
        }
        else
        {
            ScheduleManager.TimeData highTimeData = dataManager.data.HighTime;
            string highTime = "";
            highTime += highTimeData.Day.ToString() + "日 ";
            highTime += highTimeData.Hour.ToString("d2") + ":";
            highTime += highTimeData.Min.ToString("d2");
            highTimeText.text = highTime;
        }
    }
}
