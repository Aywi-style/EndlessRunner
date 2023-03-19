using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class InitSwipe : IEcsInitSystem
    {
        readonly EcsWorldInject _world;

        readonly EcsPoolInject<InputComponent> _inputPool = default;

        public void Init (IEcsSystems systems)
        {
            var inputEntity = _world.Value.NewEntity();

            _inputPool.Value.Add(inputEntity);
        }
    }
}