using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class HeightCounting : IEcsRunSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        private float _heightCountBuffer;

        public void Run (IEcsSystems systems)
        {
            _heightCountBuffer += _gameState.Value.AirBallonConfig.Speed * Time.deltaTime;

            var heightOvercome = Mathf.FloorToInt(_heightCountBuffer);

            if (heightOvercome <= 0)
            {
                return;
            }

            _heightCountBuffer -= heightOvercome;

            _gameState.Value.FlyingHeight += heightOvercome;
        }
    }
}