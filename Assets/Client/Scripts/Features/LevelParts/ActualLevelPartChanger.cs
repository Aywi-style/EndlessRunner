using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class ActualLevelPartChanger : IEcsRunSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        readonly EcsWorldInject _world;

        readonly EcsPoolInject<MoveTransitionBackgroundEvent> _moveTransitionBackgroundEvent = default;

        public void Run (IEcsSystems systems)
        {
            if (_gameState.Value.FlyingHeight < _gameState.Value.ActualLevelPart.EndHeight)
            {
                return;
            }

            if (_gameState.Value.LevelConfig.LevelParts.Length - 1 < _gameState.Value.ActualLevelPartIndex + 1)
            {
                return;
            }

            _gameState.Value.ActualLevelPartIndex++;

            _gameState.Value.ActualLevelPart = _gameState.Value.LevelConfig.LevelParts[_gameState.Value.ActualLevelPartIndex];

            _moveTransitionBackgroundEvent.Value.Add(_world.Value.NewEntity()).Invoke();
        }
    }
}