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

    private void Start()
    {
        startPos = transform.position;
        Vector3 destination = new Vector2(Random.Range(-23.5f, 23.5f), meteorTargetY);
        dirVector = (destination - startPos).normalized;
    }

    public void Init(Ground ground)
    {
        this.ground = ground;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        Vector3 v = dirVector * (initialSpeed + acceleration * currentTime);
        transform.position = startPos + v * currentTime;
        if (transform.position.y < meteorTargetY)
        {
            ground.Damage(1);
            Destroy(gameObject);
        }
    }
}
