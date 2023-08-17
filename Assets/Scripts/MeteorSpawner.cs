using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] float meteorSpawnY = 44f;
    [SerializeField] int meteorSpawnCount;
    [SerializeField] Meteor meteorPrefab;
    [SerializeField] ScheduleManager scheduleManager;
    [SerializeField] Ground ground;

    float meteorSpawnInterval;

    public List<Meteor> CurrentMeteors { get; private set; } = new List<Meteor>();

    int meteorSpawnCounter;
    int meteorBrokenCounter;

    float meteorSpawnTimer;

    bool canMeteorSpawn;
    bool isPause = false;

    public float GetProgress()
    {
        return Mathf.Min(meteorBrokenCounter / meteorSpawnCount, 1);
    }

    public int GetLaterMeteorCount()
    {
        return meteorSpawnCount - meteorSpawnCounter;
    }

    private void Start()
    {
        scheduleManager.DayStart += Init;
        scheduleManager.DayEnd += () => canMeteorSpawn = false;
    }

    public void Init()
    {
        canMeteorSpawn = true;
        meteorSpawnTimer = 0;
        meteorSpawnCounter = 0;
        meteorBrokenCounter = 0; 
        meteorSpawnInterval = Random.Range(
            scheduleManager.CurrentDayData.SpawnIntervalMin,
            scheduleManager.CurrentDayData.SpawnIntervalMax);
    }

    private void Update()
    {
        if (!canMeteorSpawn || isPause) return;

        meteorSpawnTimer += Time.deltaTime;
        if (meteorSpawnTimer > meteorSpawnInterval && scheduleManager.state == ScheduleManager.State.Middle)
        {
            meteorSpawnTimer = 0;
            meteorSpawnInterval = Random.Range(
                scheduleManager.CurrentDayData.SpawnIntervalMin,
                scheduleManager.CurrentDayData.SpawnIntervalMax);
            SpawnMeteor();
        }
    }

    void SpawnMeteor()
    {
        meteorSpawnCounter++;

        Vector2 spawnPos = new Vector2(Random.Range(-23.5f, 23.5f), meteorSpawnY);
        Meteor meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);

        MeteorData data = null;
        DayData.MeteorDataAndProportion[] meteors = scheduleManager.CurrentDayData.Meteors;
        float r = Random.value;
        float sum = 0;
        foreach (var m in meteors)
        {
            sum += m.Proportion;
            if (r <= sum)
            {
                data = m.MeteorData;
                break;
            }
        }

        meteor.Init(data, ground, () => 
            { 
                meteorBrokenCounter++;
                CurrentMeteors.Remove(meteor);
            }, scheduleManager, this);

        CurrentMeteors.Add(meteor);
    }

    public void Pause()
    {
        isPause = true;
        foreach (Meteor meteor in CurrentMeteors)
        {
            meteor.Pause();
        }
    }

    public void Resume()
    {
        isPause = false;
        foreach (Meteor meteor in CurrentMeteors)
        {
            meteor.Resume();
        }
    }

    public void BreakAllMeteors()
    {
        List<Meteor> meteors = new List<Meteor>(CurrentMeteors);
        foreach (Meteor meteor in meteors)
        {
            meteor.Break(true);
        }
    }
}
