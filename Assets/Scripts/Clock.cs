using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] RectTransform hand;
    [SerializeField] ScheduleManager scheduleManager;

    private void Update()
    {
        float ratio = scheduleManager.GetCurrentSumTime() / scheduleManager.DayTime;
        Vector3 angles = hand.eulerAngles;
        angles.z = -1 * ratio * 360;
        hand.eulerAngles = angles;
    }
}
