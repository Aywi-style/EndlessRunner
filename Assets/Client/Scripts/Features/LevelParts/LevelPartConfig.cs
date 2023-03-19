using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Client;

[CreateAssetMenu(fileName = "LevelPartConfig", menuName = "Configs/Level/Part")]
public class LevelPartConfig : ScriptableObject
{
    [field: SerializeField] public Sprite[] BachgroundObjectsSprites { private set; get; }
    [field: SerializeField] public Sprite[] Obstacles { private set; get; }

    [field: Space]
    [field: SerializeField] public Sprite TransitionSprite { private set; get; }
    [field: SerializeField] public Color BackgroundColor { private set; get; }
}
