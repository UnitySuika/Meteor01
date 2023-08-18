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
        if (ground.Life != currentLife)
        {
            foreach (Transform t in heartsRoot)
            {
                Destroy(t.gameObject);
            }
            for (int i = 0; i < ground.Life; i++)
            {
                Instantiate(heartPrefab, heartsRoot);
            }
            currentLife = ground.Life;
        }
    }
}
