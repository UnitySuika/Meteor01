using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TurretInputByButtonAndSlider : MonoBehaviour, TurretInput
{
    [SerializeField] RectTransform canvasRect;
    [SerializeField] RectTransform elementsRoot;
    [SerializeField] RectTransform sliderPrefab;
    [SerializeField] Button shootButtonPrefab;
    RectTransform slider;
    RectTransform sliderHandle;
    Button shootButton;

    public Action Shoot { get; set; }

    float angle;

    public void Init()
    {
        slider = Instantiate(sliderPrefab, elementsRoot);
        sliderHandle = (RectTransform)slider.Find("Handle");
        shootButton = Instantiate(shootButtonPrefab, elementsRoot);
        shootButton.onClick.AddListener(() => Shoot());
    }

    void Stop()
    {
        Debug.Log("Disable");
        Destroy(slider.gameObject);
        Destroy(shootButton.gameObject);
        Debug.Log("Disable");
    }

    public float GetAngle()
    {
        return angle;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // 座標が仮想スライダーのどの位置にあるか
            float clickPosX = Input.touches.Length > 0 ? Input.touches[0].position.x : Input.mousePosition.x;
            float mouseUIPosX = clickPosX * canvasRect.sizeDelta.x / Screen.width - Screen.width / 2f;
            float sliderLeft = slider.anchoredPosition.x - slider.sizeDelta.x / 2f;
            float x = (mouseUIPosX - sliderLeft) / slider.sizeDelta.x;
            if (x < 0 || x > 1) return;
            Vector2 pos = sliderHandle.anchoredPosition;
            pos.x = slider.sizeDelta.x * x;
            sliderHandle.anchoredPosition = pos;

            angle = -1 * (x - 0.5f) * 90 * 2;
        }
    }
}
