using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class StartGameSystem : IEcsInitSystem
    {
        readonly EcsWorldInject _world;

        readonly EcsPoolInject<MoveTransitionBackgroundEvent> _moveTransitionBackgroundEventPool = default;

        public void Init (IEcsSystems systems)
        {
            _moveTransitionBackgroundEventPool.Value.Add(_world.Value.NewEntity()).Invoke();
        }
    }
}