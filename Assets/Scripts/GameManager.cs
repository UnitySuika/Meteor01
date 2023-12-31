using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ground ground;
    [SerializeField] MeteorSpawner meteorSpawner;
    [SerializeField] Turret turret;
    [SerializeField] ScheduleManager scheduleManager;
    [SerializeField] DataManager dataManager;

    [SerializeField] GameOverWindow gameOverWindow;
    [SerializeField] ClearWindow clearWindow;
    [SerializeField] PauseWindow pauseWindow;

    public bool IsGameOver => ground.Life == 0;
    public bool IsClear => scheduleManager.IsLastDayEnd;
    public bool isGameEnd { get; private set; } = false;

    public int Score { get; private set; }
    public bool HighScoreUpdated = false;
    public bool HighTimeUpdated = false;

    public Action<int> bonusAdded;

    public void AddScore(int value)
    {
        if (value > 10 && bonusAdded != null)
        {
            bonusAdded(value - 10);
        }
        Score += value;
    }

    private void Start()
    {
        Score = 0;
        //turret.BreakMeteor += (meteor) => Score += meteor.Score;

        scheduleManager.DayStart += () =>
        {
            meteorSpawner.Init();
        };
    }

    private void Update()
    {
        if (!isGameEnd)
        {
            if (IsGameOver)
            {
                isGameEnd = true;
                if (Score > dataManager.data.HighScore)
                {
                    dataManager.data.HighScore = Score;
                    HighScoreUpdated = true;
                }
                ScheduleManager.TimeData timeData = scheduleManager.GetTime();
                if (scheduleManager.GetLongerTime(timeData, dataManager.data.HighTime)
                    == timeData)
                {
                    dataManager.data.HighTime = timeData;
                    HighTimeUpdated = true;
                }
                dataManager.Save();
                gameOverWindow.gameObject.SetActive(true);
                gameOverWindow.Init();
                PauseGame();
            }
            else if (IsClear)
            {
                isGameEnd = true;
                if (Score > dataManager.data.HighScore)
                {
                    dataManager.data.HighScore = Score;
                    HighScoreUpdated = true;
                }
                ScheduleManager.TimeData timeData = scheduleManager.GetTime();
                if (scheduleManager.GetLongerTime(timeData, dataManager.data.HighTime)
                    == timeData)
                {
                    dataManager.data.HighTime = timeData;
                    HighTimeUpdated = true;
                }
                clearWindow.gameObject.SetActive(true);
                clearWindow.Init();
                PauseGame();
            }
        }
    }

    public void Retry()
    {
        isGameEnd = false;
        Score = 0;
        HighScoreUpdated = false;
        HighTimeUpdated = false;
        gameOverWindow.gameObject.SetActive(false);
        clearWindow.gameObject.SetActive(false);
        ground.Init();
        scheduleManager.Init();
        ResumeGame();
    }

    public void ToTitle()
    {
        FadeManager.Instance.FadeAndLoad(0.5f, "TitleScene", 0.5f).Forget();
    }

    public void PauseGame()
    {
        meteorSpawner.Pause();
        turret.Pause();
        scheduleManager.Pause();
    }

    public void ResumeGame()
    {
        meteorSpawner.Resume();
        turret.Resume();
        scheduleManager.Resume();
    }

    public void OnPauseButton()
    {
        pauseWindow.gameObject.SetActive(true);
        PauseGame();
    }
}
