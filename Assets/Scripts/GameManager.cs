using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ground ground;
    [SerializeField] MeteorSpawner meteorSpawner;
    [SerializeField] Turret turret;
    [SerializeField] ScheduleManager scheduleManager;

    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject clearCanvas;

    bool IsGameOver => ground.Life == 0;
    bool IsClear => scheduleManager.IsLastDayEnd;
    bool isGameEnd = false;

    public int Score { get; private set; }
    public int Money { get; private set; }

    private void Start()
    {
        Score = 0;
        turret.HitMeteor += (meteor) => Score += meteor.Score;
        turret.HitMeteor += (meteor) => Money += meteor.Price;
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
}
