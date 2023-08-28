using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusSign : MonoBehaviour
{
    [SerializeField] float fadeTime;

    float currentTime = 0f;
    float initialTransparency;
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        initialTransparency = text.color.a;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        Color color = text.color;
        color.a = Mathf.Lerp(initialTransparency, 0, currentTime / fadeTime);
        text.color = color;

        if (color.a <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
