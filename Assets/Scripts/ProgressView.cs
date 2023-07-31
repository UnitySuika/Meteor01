using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressView : MonoBehaviour
{
    [SerializeField] MeteorSpawner meteorSpawner;

    [SerializeField] TextMeshProUGUI text;

    private void Update()
    {
        float progress = meteorSpawner.GetProgress() * 100;
        text.text = progress.ToString("0") + "%";
    }
}
