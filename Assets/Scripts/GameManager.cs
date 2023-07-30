using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ground ground;

    [SerializeField] GameObject gameOverCanvas;

    bool IsGameOver => ground.Life == 0;

    private void Update()
    {
        if (IsGameOver)
        {
            gameOverCanvas.SetActive(true);
        }
    }
}
