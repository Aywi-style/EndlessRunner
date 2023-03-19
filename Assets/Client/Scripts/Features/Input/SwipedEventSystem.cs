using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;

namespace Client
{
    sealed class SwipedEventSystem : IEcsRunSystem
    {        
        readonly EcsFilterInject<Inc<SwipedEvent>> _eventFilter = default;
        readonly EcsFilterInject<Inc<UnderPlayerControlle>> _underPlayerControlleFilter = default;

        readonly EcsPoolInject<SwipedEvent> _eventPool = default;
        readonly EcsPoolInject<UnderPlayerControlle> _underPlayerControllePool = default;

        private int _eventEntity = -1;

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

                ref var swipedEvent = ref _eventPool.Value.Get(_eventEntity);

                foreach (var controlledEnity in _underPlayerControlleFilter.Value)
                {
                    ref var underPlayerControlle = ref _underPlayerControllePool.Value.Get(controlledEnity);

                    var targetLine = (int)underPlayerControlle.TargetLine + (int)swipedEvent.Direction;
                    targetLine = Mathf.Clamp(targetLine, (int)LineType.Left, (int)LineType.Right);

                    underPlayerControlle.TargetLine = (LineType)targetLine;
                    underPlayerControlle.CurrentTimeToMove = underPlayerControlle.MaxTimeToMove;
                }

                DeleteEvent();
            }
        }

        private void DeleteEvent()
        {
            _eventPool.Value.Del(_eventEntity);

            _eventEntity = -1;
        }
    }
}