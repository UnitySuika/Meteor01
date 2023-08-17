using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DayData
{
    public MeteorDataAndProportion[] Meteors => _Meteors;
    public float SpawnIntervalMin => _SpawnIntervalMin;
    public float SpawnIntervalMax => _SpawnIntervalMax;

    [SerializeField] MeteorDataAndProportion[] _Meteors;
    [SerializeField] float _SpawnIntervalMin;
    [SerializeField] float _SpawnIntervalMax;

    [System.Serializable]
    public class MeteorDataAndProportion
    {
        public MeteorData MeteorData;
        public float Proportion;
    }
}
