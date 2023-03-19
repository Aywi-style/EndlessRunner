using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class ScrollingSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        readonly EcsFilterInject<Inc<Scrollable, View>> _scrollableFilter = default;

        readonly EcsPoolInject<Scrollable> _scrollablePool = default;
        readonly EcsPoolInject<View> _viewPool = default;

        public void Run (IEcsSystems systems)
        {
            foreach (var scrollableEntity in _scrollableFilter.Value)
            {
                ref var view = ref _viewPool.Value.Get(scrollableEntity);
                ref var scrollable = ref _scrollablePool.Value.Get(scrollableEntity);

                float scrollValue = _gameState.Value.AirBallonConfig.Speed * scrollable.Factor * Time.deltaTime;

                view.Transform.Translate(0, -scrollValue, 0);
            }
        }
    }
}