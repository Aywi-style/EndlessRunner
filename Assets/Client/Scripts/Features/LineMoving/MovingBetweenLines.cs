using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using DG.Tweening;

namespace Client
{
    sealed class MovingBetweenLines : IEcsRunSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        readonly EcsFilterInject<Inc<UnderPlayerControlle, View>> _underPlayerControlleFilter = default;

        readonly EcsPoolInject<UnderPlayerControlle> _underPlayerControllePool = default;
        readonly EcsPoolInject<View> _viewPool = default;

        private const float c_timerEnd = 0f;

        private int _controlledEntity = GameState.NULL_ENTITY;

        public void Run (IEcsSystems systems)
        {
            foreach (var controlledEntity in _underPlayerControlleFilter.Value)
            {
                _controlledEntity = controlledEntity;

                ref var underPlayerControlle = ref _underPlayerControllePool.Value.Get(_controlledEntity);

                if (underPlayerControlle.CurrentTimeToMove <= c_timerEnd)
                {
                    _controlledEntity = GameState.NULL_ENTITY;

                    continue;
                }

                underPlayerControlle.CurrentTimeToMove -= Time.deltaTime;

                ref var view = ref _viewPool.Value.Get(_controlledEntity);

                Vector3 newPosition = view.Transform.position;
                float targetPositionX = _gameState.Value.GameSettings.LineOffset * (float)underPlayerControlle.TargetLine;
                newPosition.x = Mathf.Lerp(view.Transform.position.x, targetPositionX, (underPlayerControlle.MaxTimeToMove - underPlayerControlle.CurrentTimeToMove) / underPlayerControlle.MaxTimeToMove);

                view.Transform.position = newPosition;

                if (underPlayerControlle.CurrentTimeToMove <= c_timerEnd)
                {
                    underPlayerControlle.CurrentLine = underPlayerControlle.TargetLine;
                    underPlayerControlle.CurrentTimeToMove = c_timerEnd;
                }

                _controlledEntity = GameState.NULL_ENTITY;
            }
        }
    }
}