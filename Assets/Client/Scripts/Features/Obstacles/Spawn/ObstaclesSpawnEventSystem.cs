using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class ObstaclesSpawnEventSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        readonly EcsWorldInject _world;

        readonly EcsFilterInject<Inc<ObstaclesSpawnEvent>> _eventFilter = default;
        
        readonly EcsPoolInject<ObstaclesSpawnEvent> _eventPool = default;

        readonly EcsPoolInject<Obstacle> _obstaclePool = default;
        readonly EcsPoolInject<Scrollable> _scrollablePool = default;
        readonly EcsPoolInject<View> _viewPool = default;

        private int _eventEntity = GameState.NULL_ENTITY;
        private int _obstacleEntity = GameState.NULL_ENTITY;

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

                ObstacleSpawning();

                DeleteEvent();
            }
        }

        private void ObstacleSpawning()
        {
            ref var obstaclesSpawnEvent = ref _eventPool.Value.Get(_eventEntity);

            _obstacleEntity = _world.Value.NewEntity();

            ref var obstacle = ref _obstaclePool.Value.Add(_obstacleEntity);
            if (_gameState.Value.ActivePools.Obstacle.GetFromPool().TryGetComponent(out ObstacleMB obstacleMB))
            {
                obstacle.ObstacleMB = obstacleMB;
            }
            else
            {
                Debug.LogError("Obstacle havent ObstacleMB!");
                return;
            }

            ref var view = ref _viewPool.Value.Add(_obstacleEntity);
            view.GameObject = obstacle.ObstacleMB.gameObject;
            view.Transform = obstacle.ObstacleMB.transform;
            view.ViewSprite = obstacle.ObstacleMB.ViewSprite;

            ref var scrollable = ref _scrollablePool.Value.Add(_obstacleEntity);
            scrollable.Factor = obstacle.ObstacleMB.ScrollFactor;

            view.Transform.position = ChoosePosition();

            int randomMax = _gameState.Value.ActualLevelPart.LevelPartConfig.Obstacles.Length;
            view.ViewSprite.sprite = _gameState.Value.ActualLevelPart.LevelPartConfig.Obstacles[Random.Range(0, randomMax)];
            view.GameObject.SetActive(true);
        }

        private Vector3 ChoosePosition()
        {
            int line = Random.Range((int)LineType.Left, (int)LineType.Right + 1);
            
            float positionX = _gameState.Value.GameSettings.LineOffset * line;
            float positionY = ScreenSize.Max.y * 1.1f;

            return new Vector3(positionX, positionY, 0);
        }

        private void DeleteEvent()
        {
            _eventPool.Value.Del(_eventEntity);

            _eventEntity = GameState.NULL_ENTITY;
            _obstacleEntity = GameState.NULL_ENTITY;
        }
    }
}