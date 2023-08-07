using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] float meteorSpawnY = 44f;
    [SerializeField] float meteorSpawnInterval;
    [SerializeField] int meteorSpawnCount;
    [SerializeField] Meteor meteorPrefab;
    [SerializeField] ScheduleManager scheduleManager;
    [SerializeField] Ground ground;

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
    }

    private void Update()
    {
        if (!canMeteorSpawn || isPause) return;

        meteorSpawnTimer += Time.deltaTime;
        if (meteorSpawnTimer > meteorSpawnInterval && scheduleManager.state == ScheduleManager.State.Middle)
        {
            meteorSpawnTimer = 0;
            SpawnMeteor();
        }
    }

    void SpawnMeteor()
    {
        meteorSpawnCounter++;

        Vector2 spawnPos = new Vector2(Random.Range(-23.5f, 23.5f), meteorSpawnY);
        Meteor meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
        meteor.Init(ground, () => 
            { 
                meteorBrokenCounter++;
                CurrentMeteors.Remove(meteor);
            }, scheduleManager);

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
}
