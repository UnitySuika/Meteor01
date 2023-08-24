using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

public class ScheduleManager : MonoBehaviour
{
    public float DayTime => dayTime;
    [SerializeField] float dayTime;
    [SerializeField] int maxDay;

    [SerializeField] float curtainFadeInTime;
    [SerializeField] float curtainMiddleTime;
    [SerializeField] float curtainFadeOutTime;
    [SerializeField] Image curtain;

    [SerializeField] float daySpeedUpRatio = 1.2f;

    [SerializeField] DayData[] dayDatas;

    [SerializeField, Header("テスト用")] int startDay = 1;
    [Space(10)]

    [SerializeField] GameManager gameManager;

    public DayData CurrentDayData => dayDatas[Day - 1];

    TextMeshProUGUI curtainDayText;

    public int Day { get; private set; }
    float currentTime;

    public Action DayStart;
    public Action DayEnd;

    public bool IsPlaying => state == State.Middle;

    public bool IsLastDayEnd { get; private set; }

    bool isPause = false;

    public enum State
    {
        Curtain,
        Middle
    }

    public State state { get; private set; }
    bool isStateStart;

    [System.Serializable]
    public class TimeData
    {
        public int Day;
        public int Hour;
        public int Min;
    }

    public TimeData GetTime()
    {
        TimeData data = new TimeData();
        data.Day = Day;
        data.Hour = Mathf.FloorToInt(currentTime / dayTime * 24f);
        float hourTime = dayTime / 24f;
        float minuteTime = hourTime / 60f;
        data.Min = Mathf.FloorToInt((currentTime % hourTime) / minuteTime);
        return data;
    }

    public float GetCurrentSumTime()
    {
        return currentTime;
    }

    public TimeData GetLongerTime(TimeData a, TimeData b)
    {
        float sumMin_a = a.Day * 24f * 60f + a.Hour * 60f + a.Min;
        float sumMin_b = b.Day * 24f * 60f + b.Hour * 60f + b.Min;
        if (sumMin_a > sumMin_b)
        {
            return a;
        }
        else if (sumMin_a < sumMin_b)
        {
            return b;
        }
        else
        {
            return null;
        }
    }

    public TimeData GetShorterTime(TimeData a, TimeData b)
    {
        float sumMin_a = a.Day * 24f * 60f + a.Hour * 60f + a.Min;
        float sumMin_b = b.Day * 24f * 60f + b.Hour * 60f + b.Min;
        if (sumMin_a > sumMin_b)
        {
            return b;
        }
        else if (sumMin_a < sumMin_b)
        {
            return a;
        }
        else
        {
            return null;
        }
    }

    public float GetCurrentSpeedUpRatio()
    {
        return Mathf.Pow(daySpeedUpRatio, Day - 1);
    }

    void SetState(State state)
    {
        this.state = state;
        isStateStart = true;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        IsLastDayEnd = false;
        Day = startDay;
        currentTime = 0;
        SetState(State.Curtain);
    }

    private void Update()
    {
        if (state == State.Curtain)
        {
            if (isStateStart)
            {
                isStateStart = false;
                Curtain().Forget();
            }
        }
        else
        {
            if (IsLastDayEnd || gameManager.IsGameOver || isPause) return;

            currentTime += Time.deltaTime;

            if (currentTime > dayTime)
            {
                if (DayEnd != null) DayEnd();
                if (Day == maxDay)
                {
                    IsLastDayEnd = true;
                    return;
                }
                currentTime = 0;
                Day++;
                SetState(State.Curtain);
            }
        }
    }

    async UniTask Curtain()
    {
        curtain.gameObject.SetActive(true);

        float timer = 0f;
        if (curtainFadeInTime == 0)
        {
            Color color = curtain.color;
            color.a = 1;
            curtain.color = color;
        }
        else
        {
            while (timer < curtainFadeInTime)
            {
                timer += Time.deltaTime;
                Color color = curtain.color;
                color.a = Mathf.Lerp(0, 1, timer / curtainFadeInTime);
                curtain.color = color;
                await UniTask.Yield();
            }
        }

        curtainDayText = curtain.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        curtainDayText.gameObject.SetActive(true);
        curtainDayText.text = Day.ToString() + "日目";

        await UniTask.Delay(TimeSpan.FromSeconds(curtainMiddleTime));

        curtainDayText.gameObject.SetActive(false);

        timer = 0f;
        while (timer < curtainFadeOutTime)
        {
            timer += Time.deltaTime;
            Color color = curtain.color;
            color.a = Mathf.Lerp(1, 0, timer / curtainFadeOutTime);
            curtain.color = color;
            await UniTask.Yield();
        }
        curtain.gameObject.SetActive(false);

        SetState(State.Middle);
        if (DayStart != null) DayStart();
    }

    public void Pause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }
}
