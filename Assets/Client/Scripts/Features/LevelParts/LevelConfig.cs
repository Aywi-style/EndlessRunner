using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level/MainConfig")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField] public int MaxBackgroundObjectsCount { private set; get; }
    [field: SerializeField] public float TimeToChangePart { private set; get; }

    [field: Space]
    [field: SerializeField] public LevelPart[] LevelParts { private set; get; }
}

[Serializable]
public class LevelPart
{
    [field: SerializeField] public string Name { private set; get; }
    [field: SerializeField] public int EndHeight { private set; get; }
    [field: SerializeField] public LevelPartConfig LevelPartConfig { private set; get; }
}