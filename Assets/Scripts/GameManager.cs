using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ground ground;
    [SerializeField] MeteorSpawner meteorSpawner;
    [SerializeField] Turret turret;

    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject clearCanvas;

    bool IsGameOver => ground.Life == 0;
    bool IsClear => meteorSpawner.GetProgress() == 1;
    bool isGameEnd = false;

    public int Score { get; private set; }

    private void Start()
    {
        Score = 0;
        turret.HitMeteor += (meteor) => Score += meteor.Score;
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
        meteorSpawner.Init();
    }
}
