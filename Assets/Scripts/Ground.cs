using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] int maxLife;
    public int Life { get; private set; }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        Life = maxLife;
    }

    public void Damage(int damage)
    {
        Life = Mathf.Max(Life - damage, 0);
    }

    public void Cure(int value)
    {
        Life = Mathf.Min(Life + value, maxLife);
    }
}
