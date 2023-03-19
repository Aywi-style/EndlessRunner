using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class AirBalloonCollidingSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        readonly EcsWorldInject _world;

        readonly EcsFilterInject<Inc<AirBalloon, View, Collidable>> _airBalloonFilter = default;

        readonly EcsPoolInject<PlayExplosionEvent> _playExplosionEventPool = default;

        readonly EcsPoolInject<View> _viewPool = default;
        readonly EcsPoolInject<Collidable> _collidablePool = default;

        private Collider[] _collidingResult;

        private int _obstaclesMask = LayerMask.GetMask(LayersList.OBSTACLE);
        private int _collidingCount;

        private bool _isFirstWork = true;

        public void Run (IEcsSystems systems)
        {
            if (_isFirstWork)
            {
                _collidingResult = new Collider[3];

                _isFirstWork = false;
            }

            foreach (var airBalloonEntity in _airBalloonFilter.Value)
            {
                ref var collidable = ref _collidablePool.Value.Get(airBalloonEntity);
                _collidingCount = Physics.OverlapSphereNonAlloc(collidable.CollidingCenter.position, collidable.CollidingRadius, _collidingResult, _obstaclesMask);

                if (_collidingCount <= 0)
                {
                    continue;
                }

                ref var view = ref _viewPool.Value.Get(airBalloonEntity);

                _playExplosionEventPool.Value.Add(_world.Value.NewEntity()).Invoke(view.Transform.position);

                _gameState.Value.GameOver();

                _collidingCount = 0;
            }
        }
    }
}