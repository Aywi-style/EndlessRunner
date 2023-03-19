using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class ObstaclesDespawn : IEcsRunSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        readonly EcsWorldInject _world;

        readonly EcsFilterInject<Inc<Obstacle, View>> _obstacleFilter = default;

        readonly EcsPoolInject<View> _viewPool = default;

        private float _despawnHeight;

        private int _obstacleEntity = GameState.NULL_ENTITY;

        private bool _isFirstWork = true;

        public void Run (IEcsSystems systems)
        {
            if (_isFirstWork)
            {
                var ScreenSizeY = ScreenSize.Max.y - ScreenSize.Min.y;

                _despawnHeight = ScreenSize.Min.y - (ScreenSizeY * 0.1f);

                _isFirstWork = false;
            }

            foreach (var obstacleEntity in _obstacleFilter.Value)
            {
                _obstacleEntity = obstacleEntity;

                ref var view = ref _viewPool.Value.Get(_obstacleEntity);

                if (view.Transform.position.y <= _despawnHeight)
                {
                    Despawn(ref view);
                }
            }
        }

        private void Despawn(ref View view)
        {
            view.GameObject.SetActive(false);
            _gameState.Value.ActivePools.Obstacle.ReturnToPool(view.GameObject);
            _world.Value.DelEntity(_obstacleEntity);

            _gameState.Value.SpawnedObstacles--;

            _obstacleEntity = GameState.NULL_ENTITY;
        }
    }
}