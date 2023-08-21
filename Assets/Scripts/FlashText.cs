using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashText : MonoBehaviour
{
    [SerializeField] float cycle = 1f;
    TextMeshProUGUI text;

    float currentTime;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        Color c = text.color;
        c.a = Mathf.Cos(2 * Mathf.PI * currentTime / cycle) / 2f + 0.5f;
        text.color = c;
    }
}
