using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] float meteorTargetY = -39f;
    [SerializeField] float bonusY = -5f;

    [SerializeField] RuntimeAnimatorController meteorAnimator;
    [SerializeField] RuntimeAnimatorController heartAnimator;

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

    GameManager gameManager;

    int maxHp = 2;

    bool isPause = false;

    SpriteRenderer spriteRenderer;

    float spawnPosMin;
    float spawnPosMax;

    const float amplitude = 10f;
    const float period = 1f;
    const float waveLength = 3f;

    bool isBroken = false;

    Animator animator;

    public void Init(MeteorData data, Ground ground, Action broken, ScheduleManager scheduleManager, float spawnPosMin, float spawnPosMax, float meteorSpawnY, MeteorSpawner meteorSpawner, GameManager gameManager)
    {
        animator = GetComponent<Animator>();

        this.data = data;
        this.ground = ground;
        this.broken = broken;
        this.scheduleManager = scheduleManager;
        this.meteorSpawner = meteorSpawner;
        this.gameManager = gameManager;
        this.spawnPosMin = spawnPosMin;
        this.spawnPosMax = spawnPosMax;

        // データの各項目の反映

        maxHp = data.Hp;
        initialSpeed = data.InitialSpeed * scheduleManager.GetCurrentSpeedUpRatio();
        acceleration = data.Acceleration;
        atk = data.Atk;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.Sprite;
        transform.localScale = Vector3.one * data.Scale;
        Reward = data.Reward;

        animator.runtimeAnimatorController = data.Name == "ハート隕石" ? heartAnimator : meteorAnimator;

        Hp = maxHp;

        if (data.Effect == MeteorData.EffectType.Turn)
        {
            float dif = amplitude + transform.localScale.x * 7f / 2f;
            Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(spawnPosMin + dif, spawnPosMax - dif), meteorSpawnY);
            transform.position = spawnPos;
            startPos = spawnPos;

            dirVector = Vector3.down;
        }
        else
        {
            Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(spawnPosMin, spawnPosMax), meteorSpawnY);            
            transform.position = spawnPos;
            startPos = spawnPos;

            Vector3 destination = new Vector2(UnityEngine.Random.Range(-23.5f, 23.5f), meteorTargetY);
            dirVector = (destination - startPos).normalized;
        }
    }

    private void Update()
    {
        if (isPause || isBroken) return;

        if (scheduleManager.state != ScheduleManager.State.Middle)
        {
            Break(false);
            return;
        }

        currentTime += Time.deltaTime;

        if (data.Effect == MeteorData.EffectType.Turn)
        {
            Vector3 v = dirVector * (initialSpeed + acceleration * currentTime);
            float y = startPos.y - transform.position.y;
            float x = amplitude * Mathf.Sin(2 * Mathf.PI * (currentTime / period));

            transform.position = startPos + Vector3.right * x + v * currentTime;
            if (transform.position.y < meteorTargetY)
            {
                ground.Damage(atk);
                Break(false);
            }
        }
        else
        {
            Vector3 v = dirVector * (initialSpeed + acceleration * currentTime);
            transform.position = startPos + v * currentTime;
            if (transform.position.y < meteorTargetY)
            {
                ground.Damage(atk);
                Break(false);
            }
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
            if (transform.position.y < bonusY)
            {
                gameManager.AddScore(20);
            }
            else
            {
                gameManager.AddScore(10);
            }

            if (data.Effect == MeteorData.EffectType.Cure)
            {
                ground.Cure(1);
            }
            else if (data.Effect == MeteorData.EffectType.Bomb)
            {
                meteorSpawner.BreakAllMeteors();
            }
        }
        isBroken = true;
        animator.SetTrigger("Break");
    }

    public void Pause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }

    public void AnimationOver()
    {
        Destroy(gameObject);
    }
}
