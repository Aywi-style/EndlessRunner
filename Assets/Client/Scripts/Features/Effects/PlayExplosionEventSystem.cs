using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class PlayExplosionEventSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        readonly EcsFilterInject<Inc<PlayExplosionEvent>> _eventFilter = default;
        
        readonly EcsPoolInject<PlayExplosionEvent> _eventPool = default;

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

                ref var playExplosionEvent = ref _eventPool.Value.Get(_eventEntity);

                GameObject.Instantiate(_gameState.Value.AirBallonConfig.Explosion, playExplosionEvent.Position, Quaternion.identity).Play();

                DeleteEvent();
            }
        }

        private void DeleteEvent()
        {
            _eventPool.Value.Del(_eventEntity);

            _eventEntity = GameState.NULL_ENTITY;
        }
    }
}