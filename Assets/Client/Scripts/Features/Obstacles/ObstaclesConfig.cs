using UnityEngine;

[CreateAssetMenu(fileName = "ObstaclesConfig", menuName = "Configs/ObstaclesConfig")]
public class ObstaclesConfig : ScriptableObject
{
    [field: SerializeField] public int MaxCountOnScreen { private set; get; }
    [field: SerializeField] public float SpawnerTimeValue { private set; get; }
    [field: SerializeField] public ObstacleMB[] ObstaclesMBs { private set; get; }
}