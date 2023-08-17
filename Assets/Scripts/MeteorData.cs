using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeteorData", menuName = "ScriptableObject/MeteorData")]
public class MeteorData : ScriptableObject
{
    [SerializeField] string _Name;
    [SerializeField, Multiline(3)] string _Description;
    [SerializeField] int _Hp;
    [SerializeField] float _InitialSpeed;
    [SerializeField] float _Acceleration;
    [SerializeField] int _Level;
    [SerializeField] int _Atk;
    [SerializeField] Sprite _Sprite;
    [SerializeField] float _Scale;
    [SerializeField] int _Reward;
    [SerializeField] EffectType _Effect;

    public enum EffectType
    {
        None,
        Cure,
        Bomb
    }

    public string Name => _Name;
    public string Description => _Description;
    public int Hp => _Hp;
    public float InitialSpeed => _InitialSpeed;
    public float Acceleration => _Acceleration;
    public int Level => _Level;
    public int Atk => _Atk;
    public Sprite Sprite => _Sprite;
    public float Scale => _Scale;
    public int Reward => _Reward;
    public EffectType Effect => _Effect;
}
