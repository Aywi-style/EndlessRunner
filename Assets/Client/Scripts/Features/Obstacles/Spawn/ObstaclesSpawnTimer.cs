using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class ObstaclesSpawnTimer : IEcsRunSystem
    {
        readonly EcsWorldInject _world;

        readonly EcsCustomInject<GameState> _gameState;

        readonly EcsPoolInject<ObstaclesSpawnEvent> _obstaclesSpawnEvent = default;

        private float _spawnTimer = 0f;

        public void Run (IEcsSystems systems)
        {
            if (_gameState.Value.SpawnedObstacles >= _gameState.Value.ObstaclesConfig.MaxCountOnScreen)
            {
                return;
            }

            if (_spawnTimer < _gameState.Value.ObstaclesConfig.SpawnerTimeValue)
            {
                _spawnTimer += Time.deltaTime;
                return;
            }

            var initerEventEntity = _world.Value.NewEntity();

            ref var obstaclesSpawnEvent = ref _obstaclesSpawnEvent.Value.Add(initerEventEntity);

            _gameState.Value.SpawnedObstacles++;
            _spawnTimer = 0;
        }
    }
}