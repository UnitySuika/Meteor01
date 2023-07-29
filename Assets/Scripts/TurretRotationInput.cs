using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotationInput : MonoBehaviour
{
    [SerializeField, Range(0, 0.5f)] float sliderHalfLength;

    bool isDragging;

    float initialX;

    float angle;

    public float GetRotation()
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
                angle = 0; 
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
                // �N���b�N���ꂽ�ʒu��0�`1�͈̔͂ɒ���
                float x = GetScreenRatio(Input.mousePosition.x);
                // �h���b�O�ł���͈́i���z�X���C�_�[�j����ʓ��ɓ��肫��Ȃ��Ȃ玸�s
                if (x - sliderHalfLength < 0 || x + sliderHalfLength > 1) return;
                initialX = x;
                isDragging = true;
            }
        }
    }

    // ��ʂ�X���W����ʑS�̂�0�`1�Ƃ����͈͂ɒ���
    float GetScreenRatio(float screenX)
    {
        return screenX / Screen.width;
    }
}
