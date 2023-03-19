using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverCanvasMB : MonoBehaviour
{
    [SerializeField] private Canvas _self;

    [Space]
    [SerializeField] private Image _background;
    [SerializeField] private float _backgroundTimeToShow;

    [Space]
    [SerializeField] private Image _title;
    [SerializeField] private float _titleTimeToShow;

    private Sequence _sequence;

    private void Awake()
    {
        GameState.OnGameOvering += Show;
    }

    private void OnDestroy()
    {
        GameState.OnGameOvering -= Show;
    }

    private void Show()
    {
        _self.enabled = true;

        _sequence.Append(_background.DOFade(0.7f, _backgroundTimeToShow));
        _sequence.Append(_title.DOFade(1f, _titleTimeToShow));
        _sequence.Append(_title.transform.DOScale(1f, _titleTimeToShow));
        _sequence.SetUpdate(true);
    }
}
