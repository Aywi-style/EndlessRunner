using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using DG.Tweening;

namespace Client
{
    sealed class MoveTransitionBackgroundEventSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        readonly EcsWorldInject _world;

        readonly EcsFilterInject<Inc<MoveTransitionBackgroundEvent>> _eventFilter = default;
        readonly EcsFilterInject<Inc<InRunCanvas>> _inRunCanvasFilter = default;

        readonly EcsPoolInject<MoveTransitionBackgroundEvent> _eventPool = default;
        readonly EcsPoolInject<ChangeBackgroundColorEvent> _changeBackgroundColorEventPool = default;

        readonly EcsPoolInject<InRunCanvas> _inRunCanvasPool = default;

        private bool _isFirstEvent = true;

        private int _eventEntity = GameState.NULL_ENTITY;

        public void Run (IEcsSystems systems)
        {
            var entitiesCount = _eventFilter.Value.GetEntitiesCount();

            if (entitiesCount <= 0)
            {
                return;
            }

            var rawEntities = _eventFilter.Value.GetRawEntities();

            for (int i = 0; i < entitiesCount; i++)
            {
                _eventEntity = rawEntities[i];

                foreach (var inRunCanvasEntity in _inRunCanvasFilter.Value)
                {
                    ref var inRunCanvas = ref _inRunCanvasPool.Value.Get(inRunCanvasEntity);

                    if (_isFirstEvent)
                    {
                        inRunCanvas.InRunCanvasMB.TransitionBackground.DOMove(inRunCanvas.InRunCanvasMB.TransitionFinish.position, _gameState.Value.LevelConfig.TimeToChangePart, false);

                        _isFirstEvent = false;
                        DeleteEvent();

                        continue;
                    }

                    inRunCanvas.InRunCanvasMB.TransitionBackgroundImage.preserveAspect = false;
                    inRunCanvas.InRunCanvasMB.TransitionBackgroundImage.sprite = _gameState.Value.ActualLevelPart.LevelPartConfig.TransitionSprite;
                    inRunCanvas.InRunCanvasMB.TransitionBackground.position = inRunCanvas.InRunCanvasMB.TransitionStart.position;
                    inRunCanvas.InRunCanvasMB.TransitionBackground.DOMove(inRunCanvas.InRunCanvasMB.TransitionFinish.position, _gameState.Value.LevelConfig.TimeToChangePart, false);

                    _changeBackgroundColorEventPool.Value.Add(_world.Value.NewEntity()).Invoke();

                    DeleteEvent();
                }
            }
        }

        private void DeleteEvent()
        {
            _eventPool.Value.Del(_eventEntity);

            _eventEntity = GameState.NULL_ENTITY;
        }
    }
}