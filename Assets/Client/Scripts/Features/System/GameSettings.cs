using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Configs/GameSettings")]
public class GameSettings : ScriptableObject
{
    [field: SerializeField] public float LineOffset { private set; get; }
    [field: SerializeField] public float TimeToRestart { private set; get; }
    [field: SerializeField] public float SwipeSensitivity { private set; get; }
    [field: SerializeField] public float SwipeDelay { private set; get; }
}