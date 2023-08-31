using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] int maxLife;

    [SerializeField] float shakeTime;
    [SerializeField] float shakeCount;
    [SerializeField] float shakePower;
    [SerializeField] Transform[] groundObjects;
    [SerializeField] Vector3[] groundObjectsPosition;
    public int Life { get; private set; }

    private void Start()
    {
        Init();

        groundObjectsPosition = new Vector3[groundObjects.Length];
        for (int i = 0; i < groundObjects.Length; i++)
        {
            groundObjectsPosition[i] = groundObjects[i].localPosition;
        }
    }

    public void Init()
    {
        Life = maxLife;
    }

    public void Damage(int damage)
    {
        Life = Mathf.Max(Life - damage, 0);
        StopAllCoroutines();
        StartCoroutine(ShakeObjects());
    }

    public void Cure(int value)
    {
        Life = Mathf.Min(Life + value, maxLife);
    }

    IEnumerator ShakeObjects()
    {
        float currentTime = 0;
        while (currentTime < shakeTime)
        {
            float t = currentTime / shakeTime;
            float y = Mathf.Sin(t * 2 * Mathf.PI * shakeCount) * (1 - t) * shakePower;

            for (int i = 0; i < groundObjects.Length; i++)
            {
                Vector3 p = groundObjects[i].transform.localPosition;
                p.y = groundObjectsPosition[i].y + y;
                groundObjects[i].transform.localPosition = p;
            }

            yield return null;
            currentTime += Time.deltaTime;
        }
    }
}
