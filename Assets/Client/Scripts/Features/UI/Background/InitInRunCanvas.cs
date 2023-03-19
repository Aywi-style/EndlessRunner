using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class InitInRunCanvas : IEcsInitSystem
    {
        readonly EcsWorldInject _world;

        readonly EcsPoolInject<InRunCanvas> _inRunCanvasPool = default;

        public void Init (IEcsSystems systems)
        {
            var inRunCanvasMB = GameObject.FindObjectOfType<InRunCanvasMB>();

            if (inRunCanvasMB == null)
            {
                Debug.LogError("InRunCanvasMB wasnt find!");

                return;
            }

            var inRunCanvasEntity = _world.Value.NewEntity();

            ref var inRunCanvas = ref _inRunCanvasPool.Value.Add(inRunCanvasEntity);
            inRunCanvas.InRunCanvasMB = inRunCanvasMB;
        }
    }
}