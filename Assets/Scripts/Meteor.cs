using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] float meteorTargetY = -39f;

    public int Score => 10;
    public int Reward { get; private set; }

    public int Hp { get; private set; }

    int atk;

    float initialSpeed;
    float acceleration;

    MeteorData data;

    Vector3 startPos;
    Vector3 dirVector;

    float currentTime;

    Ground ground;

    Action broken;

    ScheduleManager scheduleManager;
    
    MeteorSpawner meteorSpawner;

    int maxHp = 2;

    bool isPause = false;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        startPos = transform.position;
        Vector3 destination = new Vector2(UnityEngine.Random.Range(-23.5f, 23.5f), meteorTargetY);
        dirVector = (destination - startPos).normalized;
        Hp = maxHp;
    }

    public void Init(MeteorData data, Ground ground, Action broken, ScheduleManager scheduleManager, MeteorSpawner meteorSpawner)
    {
        this.data = data;
        this.ground = ground;
        this.broken = broken;
        this.scheduleManager = scheduleManager;
        this.meteorSpawner = meteorSpawner;

        // データの各項目の反映

        maxHp = data.Hp;
        initialSpeed = data.InitialSpeed * scheduleManager.GetCurrentSpeedUpRatio();
        acceleration = data.Acceleration;
        atk = data.Atk;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.Sprite;
        transform.localScale = Vector3.one * data.Scale;
        Reward = data.Reward;
    }

    private void Update()
    {
        if (isPause) return;

        if (scheduleManager.state != ScheduleManager.State.Middle)
        {
            Break(false);
            return;
        }

        currentTime += Time.deltaTime;
        Vector3 v = dirVector * (initialSpeed + acceleration * currentTime);
        transform.position = startPos + v * currentTime;
        if (transform.position.y < meteorTargetY)
        {
            ground.Damage(atk);
            Break(false);
        }
    }

    public bool Damage(int value)
    {
        Hp = Mathf.Max(0, Hp - value);
        float redDepth = 1f - (float)Hp / maxHp;
        spriteRenderer.color = new Color(1, 1 - redDepth, 1 - redDepth);
        if (Hp == 0)
        {
            Break(true);
        }
        return Hp == 0;
    }

    public void Break(bool isAttacked)
    {
        broken();
        if (isAttacked)
        {
            if (data.Effect == MeteorData.EffectType.Cure)
            {
                ground.Cure(1);
            }
            else if (data.Effect == MeteorData.EffectType.Bomb)
            {
                meteorSpawner.BreakAllMeteors();
            }
        }
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
