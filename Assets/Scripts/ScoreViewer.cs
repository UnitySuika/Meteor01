using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameManager gameManager;

    private void Update()
    {
        scoreText.text = $"{gameManager.Score}";
    }
}
