using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ground ground;
    [SerializeField] MeteorSpawner meteorSpawner;
    [SerializeField] Turret turret;
    [SerializeField] ScheduleManager scheduleManager;
    [SerializeField] Shop shop;

    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject clearCanvas;

    public bool IsGameOver => ground.Life == 0;
    public bool IsClear => scheduleManager.IsLastDayEnd;
    public bool isGameEnd { get; private set; } = false;

    public int Score { get; private set; }
    public int Money { get; private set; }

    private void Start()
    {
        Score = 0;
        turret.BreakMeteor += (meteor) => Score += meteor.Score;
        turret.BreakMeteor += (meteor) => Money += meteor.Reward;

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
                gameOverCanvas.SetActive(true);
            }
            else if (IsClear)
            {
                isGameEnd = true;
                clearCanvas.SetActive(true);
            }
        }
    }

    public void Retry()
    {
        isGameEnd = false;
        Score = 0;
        gameOverCanvas.SetActive(false);
        clearCanvas.SetActive(false);
        ground.Init();
        scheduleManager.Init();
    }

    public void OnShopButton()
    {
        shop.Open();
        PauseGame();
        shop.OnClose += () => ResumeGame();
    }

    public void UseMoney(int value)
    {
        Money -= value;
    }

    void PauseGame()
    {
        meteorSpawner.Pause();
        turret.Pause();
        scheduleManager.Pause();
    }

    void ResumeGame()
    {
        meteorSpawner.Resume();
        turret.Resume();
        scheduleManager.Resume();
    }
}
