using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Meteor meteorPrefab;
    [SerializeField] float meteorSpawnY = 44f;
    [SerializeField] float meteorSpawnInterval;

    float meteorSpawnTimer;

    private void Start()
    {
        SpawnMeteor();
    }

    private void Update()
    {
        meteorSpawnTimer += Time.deltaTime;
        if (meteorSpawnTimer > meteorSpawnInterval)
        {
            meteorSpawnTimer = 0;
            SpawnMeteor();
        }
    }

    void SpawnMeteor()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-23.5f, 23.5f), meteorSpawnY);
        Meteor meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
    }
}
