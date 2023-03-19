using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMB : MonoBehaviour
{
    [field: SerializeField] public SpriteRenderer ViewSprite { private set; get; }

    [field: Range(0, 2)]
    [field: SerializeField] public float ScrollFactor { private set; get; }
}
