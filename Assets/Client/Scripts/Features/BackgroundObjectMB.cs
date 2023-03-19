using UnityEngine;

public class BackgroundObjectMB : MonoBehaviour
{
    [field: Range(0, 2)]
    [field: SerializeField] public float ScrollFactor { private set; get; }

    [field: SerializeField] public SpriteRenderer SpriteRenderer { private set; get; }
}
