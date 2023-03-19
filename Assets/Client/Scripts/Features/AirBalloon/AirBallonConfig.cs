using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AirBallon", menuName = "Configs/AirBallon")]
public class AirBallonConfig : ScriptableObject
{
    [field: SerializeField] public AirBallonMB AirBallon { private set; get; }
    [field: SerializeField] public float Speed { private set; get; }
    [field: SerializeField] public float LineSwipingTime { private set; get; }
    [field: SerializeField] public float WobbleStrength { private set; get; }
    [field: SerializeField] public ParticleSystem Explosion { private set; get; }
}
