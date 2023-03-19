using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class InitPools : IEcsInitSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        Vector3 _spawnPoint = new Vector3(0f, 100f, 0f);

        public void Init (IEcsSystems systems)
        {
            _gameState.Value.ActivePools = new AllPools();

            _gameState.Value.ActivePools.Obstacle = new Pool(_gameState.Value.SharedPools.Obstacle.Prefab, _spawnPoint, _gameState.Value.ObstaclesConfig.MaxCountOnScreen, parentName: "Obstacles");

            _gameState.Value.ActivePools.BackgroundObject = new Pool(_gameState.Value.SharedPools.BackgroundObject.Prefab, _spawnPoint, _gameState.Value.LevelConfig.MaxBackgroundObjectsCount, parentName: "BackgroundObjects");
        }
    }
}