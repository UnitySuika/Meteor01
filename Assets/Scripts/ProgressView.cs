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
        int count = meteorSpawner.GetLaterMeteorCount();
        text.text = $"‚ ‚Æ {count}";
    }
}
