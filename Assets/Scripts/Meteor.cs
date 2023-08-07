using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] float meteorTargetY = -39f;
    [SerializeField] float initialSpeed;
    [SerializeField] float acceleration = 1f;

    Vector3 startPos;
    Vector3 dirVector;

    float currentTime;

    Ground ground;

    Action broken;

    ScheduleManager scheduleManager;

    public int Score => 10;
    public int Price => 5;

    bool isPause = false;

    private void Start()
    {
        startPos = transform.position;
        Vector3 destination = new Vector2(UnityEngine.Random.Range(-23.5f, 23.5f), meteorTargetY);
        dirVector = (destination - startPos).normalized;
    }

    public void Init(Ground ground, Action broken, ScheduleManager scheduleManager)
    {
        this.ground = ground;
        this.broken = broken;
        this.scheduleManager = scheduleManager;
    }

    private void Update()
    {
        if (isPause) return;

        if (scheduleManager.state != ScheduleManager.State.Middle)
        {
            Break();
            return;
        }

        currentTime += Time.deltaTime;
        Vector3 v = dirVector * (initialSpeed + acceleration * currentTime);
        transform.position = startPos + v * currentTime;
        if (transform.position.y < meteorTargetY)
        {
            ground.Damage(1);
            Break();
        }
    }

    public void Break()
    {
        broken();
        Destroy(gameObject);
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
