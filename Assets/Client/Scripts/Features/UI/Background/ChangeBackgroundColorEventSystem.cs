using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class ChangeBackgroundColorEventSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        readonly EcsFilterInject<Inc<ChangeBackgroundColorEvent>> _eventFilter = default;
        readonly EcsFilterInject<Inc<InRunCanvas>> _inRunCanvasFilter = default;

        readonly EcsPoolInject<ChangeBackgroundColorEvent> _eventPool = default;

        readonly EcsPoolInject<InRunCanvas> _inRunCanvasPool = default;

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

                    if (inRunCanvas.InRunCanvasMB.TransitionBackground.transform.position.y > inRunCanvas.InRunCanvasMB.TransitionCenter.position.y)
                    {
                        continue;
                    }

                    inRunCanvas.InRunCanvasMB.Background.color = _gameState.Value.ActualLevelPart.LevelPartConfig.BackgroundColor;

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