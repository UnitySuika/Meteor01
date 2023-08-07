using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ScheduleManager : MonoBehaviour
{
    [SerializeField] float dayTime;
    [SerializeField] int maxDay;

    [SerializeField] float curtainFadeInTime;
    [SerializeField] float curtainMiddleTime;
    [SerializeField] float curtainFadeOutTime;
    [SerializeField] Image curtain;

    [SerializeField] GameManager gameManager;
    
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
        Day = 1;
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
                StartCoroutine(Curtain());
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

    IEnumerator Curtain()
    {
        curtain.gameObject.SetActive(true);
        float timer = 0f;
        while (timer < curtainFadeInTime)
        {
            timer += Time.deltaTime;
            Color color = curtain.color;
            color.a = Mathf.Lerp(0, 1, timer / curtainFadeInTime);
            curtain.color = color;
            yield return null;
        }

        curtainDayText = curtain.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        curtainDayText.gameObject.SetActive(true);
        curtainDayText.text = Day.ToString() + "日目";
        yield return new WaitForSeconds(curtainMiddleTime);
        curtainDayText.gameObject.SetActive(false);

        timer = 0f;
        while (timer < curtainFadeOutTime)
        {
            timer += Time.deltaTime;
            Color color = curtain.color;
            color.a = Mathf.Lerp(1, 0, timer / curtainFadeOutTime);
            curtain.color = color;
            yield return null;
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
