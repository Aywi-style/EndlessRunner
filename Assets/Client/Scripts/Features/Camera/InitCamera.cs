using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class InitCamera : IEcsInitSystem
    {
        readonly EcsWorldInject _world;

        readonly EcsPoolInject<CameraComponent> _cameraPool = default;

        public void Init (IEcsSystems systems)
        {
            var cameraObject = GameObject.FindObjectOfType<Camera>();

            if (cameraObject == null)
            {
                Debug.LogWarning("Camera was not found!");

                return;
            }

            ref var cameraComponent = ref _cameraPool.Value.Add(_world.Value.NewEntity());
            cameraComponent.Camera = cameraObject;
        }
    }
}