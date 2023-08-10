using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DayData
{
    public MeteorData[] Meteors => _Meteors;
    public float SpawnIntervalMin => _SpawnIntervalMin;
    public float SpawnIntervalMax => _SpawnIntervalMax;

    [SerializeField] MeteorData[] _Meteors;
    [SerializeField] float _SpawnIntervalMin;
    [SerializeField] float _SpawnIntervalMax;
}
