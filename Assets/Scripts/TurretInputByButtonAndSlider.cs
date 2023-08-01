using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TurretInputByButtonAndSlider : MonoBehaviour, TurretInput
{
    [SerializeField] RectTransform canvasRect;
    [SerializeField] RectTransform slider;
    [SerializeField] RectTransform sliderHandle;
    [SerializeField] Button shootButton;

    public Action Shoot { get; set; }

    float angle;

    public float GetAngle()
    {
        return angle;
    }

    void Start()
    {
        shootButton.onClick.AddListener(() => Shoot());
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // 座標が仮想スライダーのどの位置にあるか
            float mouseUIPosX = Input.mousePosition.x * canvasRect.sizeDelta.x / Screen.width - Screen.width / 2f;
            float sliderLeft = slider.anchoredPosition.x - slider.sizeDelta.x / 2f;
            float sliderRight = slider.anchoredPosition.x + slider.sizeDelta.x / 2f;
            float x = (mouseUIPosX - sliderLeft) / slider.sizeDelta.x;
            if (x < 0 || x > 1) return;
            Vector2 pos = sliderHandle.anchoredPosition;
            pos.x = slider.sizeDelta.x * x;
            Debug.Log(pos.x);
            sliderHandle.anchoredPosition = pos;

            angle = -1 * (x - 0.5f) * 90 * 2;
        }
    }
}
