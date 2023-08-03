using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScheduleManager : MonoBehaviour
{
    [SerializeField] float dayTime;
    [SerializeField] int maxDay;

    public int Day { get; private set; }
    float currentTime;

    public bool IsLastDayEnd { get; private set; }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        IsLastDayEnd = false;
        Day = 1;
        currentTime = 0;
    }

    private void Update()
    {
        if (IsLastDayEnd) return;

        currentTime += Time.deltaTime;

        if (currentTime > dayTime)
        {
            if (Day == maxDay)
            {
                IsLastDayEnd = true;
                return;
            }
            currentTime = 0;
            Day++;
        }
    }
}
