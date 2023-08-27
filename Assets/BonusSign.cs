using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusSign : MonoBehaviour
{
    [SerializeField] float fadeTime;

    float currentTime = 0f;
    float initialTransparency;
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        initialTransparency = image.color.a;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        Color color = image.color;
        color.a = Mathf.Lerp(initialTransparency, 0, currentTime / fadeTime);
        image.color = color;

        if (color.a <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
