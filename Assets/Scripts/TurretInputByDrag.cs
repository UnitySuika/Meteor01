using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretInputByDrag : MonoBehaviour, TurretInput
{
    [SerializeField, Range(0, 0.5f)] float sliderHalfLength;

    [SerializeField] float returnTime;

    bool isDragging;

    float initialX;

    float angle;

    public Action Shoot { get; set; }

    float returnStartAngle;
    float returnCurrentTime;

    public float GetAngle()
    {
        return angle;
    }

    private void Update()
    {
        if (isDragging)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                returnStartAngle = angle;
                returnCurrentTime = 0;
                Shoot();
            }


            float dif = GetScreenRatio(Input.mousePosition.x) - initialX;
            if (Mathf.Abs(dif) > sliderHalfLength)
            {
                dif = dif > 0 ? sliderHalfLength : -sliderHalfLength;
            }
            angle = -1 * dif / sliderHalfLength * 90f;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                // クリックされた位置を0～1の範囲に直す
                float x = GetScreenRatio(Input.mousePosition.x);
                // ドラッグできる範囲（仮想スライダー）が画面内に入りきらないなら失敗
                if (x - sliderHalfLength < 0 || x + sliderHalfLength > 1) return;
                initialX = x;
                isDragging = true;
            }

            // 徐々に真ん中に戻す
            returnCurrentTime += Time.deltaTime;
            angle = Mathf.Lerp(returnStartAngle, 0, returnCurrentTime / returnTime);
        }
    }

    // 画面のX座標を画面全体を0～1とした範囲に直す
    float GetScreenRatio(float screenX)
    {
        return screenX / Screen.width;
    }
}
