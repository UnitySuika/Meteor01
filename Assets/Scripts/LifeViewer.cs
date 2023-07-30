using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeViewer : MonoBehaviour
{
    [SerializeField] Ground ground;
    [SerializeField] Transform heartsRoot;
    [SerializeField] GameObject heartPrefab;

    int currentLife;

    private void Update()
    {
        if (ground.Life < currentLife)
        {
            for (int i = 0; i < currentLife - ground.Life; i++)
            {
                Destroy(heartsRoot.GetChild(heartsRoot.childCount - 1).gameObject);
            }
            currentLife = ground.Life;
        }
        if (ground.Life > currentLife)
        {
            for (int i = 0; i < ground.Life - currentLife; i++)
            {
                Instantiate(heartPrefab, heartsRoot);
            }
            currentLife = ground.Life;
        }
    }
}
