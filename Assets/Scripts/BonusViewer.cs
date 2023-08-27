using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusViewer : MonoBehaviour
{
    [SerializeField] GameObject bonusSign;
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager.bonusAdded += bonus => ShowBonus();
    }

    public void ShowBonus()
    {
        Instantiate(bonusSign, transform);
    }
}
