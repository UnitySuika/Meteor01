using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] Meteor meteorPrefab;
    [SerializeField] float meteorSpawnY = 44f;
    [SerializeField] float meteorSpawnInterval;
    [SerializeField] float meteorSpawnCount;

    [SerializeField] Ground ground;
    int meteorSpawnCounter;
    int meteorBrokenCounter;

    float meteorSpawnTimer;

    public float GetProgress()
    {
        return Mathf.Min(meteorBrokenCounter / meteorSpawnCount, 1);
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        meteorSpawnTimer = 0;
        meteorSpawnCounter = 0;
        meteorBrokenCounter = 0;
        SpawnMeteor();
    }

    private void Update()
    {
        meteorSpawnTimer += Time.deltaTime;
        if (meteorSpawnTimer > meteorSpawnInterval && meteorSpawnCounter < meteorSpawnCount)
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
        meteor.Init(ground, () => meteorBrokenCounter++);
    }
}
