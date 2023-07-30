using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] int maxLife;
    public int Life { get; private set; }

    private void Start()
    {
        Life = maxLife;
    }

    public void Damage(int damage)
    {
        Life = Mathf.Max(Life - damage, 0);
    }
}
