using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyViewer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] GameManager gameManager;

    private void Update()
    {
        moneyText.text = $"{gameManager.Money} C";
    }
}
