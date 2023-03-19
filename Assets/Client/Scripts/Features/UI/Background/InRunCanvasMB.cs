using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InRunCanvasMB : MonoBehaviour
{
    [field: SerializeField] public Canvas Self { private set; get; }

    [field: SerializeField] public Image Background { private set; get; }
    [field: SerializeField] public RectTransform TransitionBackground { private set; get; }
    [field: SerializeField] public Image TransitionBackgroundImage { private set; get; }
    [field: SerializeField] public RectTransform TransitionStart { private set; get; }
    [field: SerializeField] public RectTransform TransitionCenter { private set; get; }
    [field: SerializeField] public RectTransform TransitionFinish { private set; get; }
}
