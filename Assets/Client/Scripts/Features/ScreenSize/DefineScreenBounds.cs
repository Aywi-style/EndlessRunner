using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class DefineScreenBounds : IEcsInitSystem
    {
        readonly EcsFilterInject<Inc<CameraComponent>> _cameraFilter = default;

        readonly EcsPoolInject<CameraComponent> _cameraPool = default;

        public void Init (IEcsSystems systems)
        {
            foreach (var cameraEntity in _cameraFilter.Value)
            {
                ref var cameraComponent = ref _cameraPool.Value.Get(cameraEntity);

                var screenSizeMax = cameraComponent.Camera.ViewportToWorldPoint(new Vector3(1f, 1f, -cameraComponent.Camera.transform.position.z));
                var screenSizeMin = cameraComponent.Camera.ViewportToWorldPoint(new Vector3(0f, 0f, -cameraComponent.Camera.transform.position.z));

                ScreenSize.Max = new Vector2(screenSizeMax.x, screenSizeMax.y);
                ScreenSize.Min = new Vector2(screenSizeMin.x, screenSizeMin.y);
            }
        }
    }
}